using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace service.Services;

public class LoginService
{
    private PasswordHashRepository _passwordHashRepository;
    private UserRepository _userRepository;
    private IConfiguration _config;
    private IHostEnvironment _hostEnvironment;

    /*
     * Constructor to initialize the service with required dependencies.
     */
    public LoginService(ILogger<LoginService> logger, UserRepository userRepository,
        PasswordHashRepository passwordHashRepository, IConfiguration config, IHostEnvironment hostEnvironment)
    {
       _userRepository = userRepository;
        _passwordHashRepository = passwordHashRepository;
        _config = config;
        _hostEnvironment = hostEnvironment;
    }
    
    /*
     * Authenticating a user based on provided login credentials.
     */
    public UserModel Authenticate(LoginModel model)
    {
        try
        {
            var passwordHash = _passwordHashRepository.GetByEmail(model.email);
            var hashAlgorithm = new PasswordHashService();
            var isValid = hashAlgorithm.VerifyHashedPassword(model.password, passwordHash.hash, passwordHash.salt);
            if (isValid)
            {
                return _userRepository.GetById(passwordHash.user_id);
            }
        }
        catch
        {
            throw new ValidationException("Wrong username or password");
        }
        return null;
    }

    /*
     * Generating a JWT token based on user information.
     */
    public string GenerateToken(UserModel model)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")));
        
        Console.WriteLine(_config.GetValue<string>("Jwt:Secret"));
        Console.WriteLine(_hostEnvironment.EnvironmentName);
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("Name", model.full_name),
            new Claim("Email", model.email),
            new Claim("IsAdmin", model.admin),
            new Claim("Id", model.user_id.ToString())
        };

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Jwt:Issuer"),
            _config.GetValue<string>("Jwt:Audience"),
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /*
     * Registering a new user, including password hashing and storage.
     */
    public UserModel Register(RegisterModel model)
    {
        try
        {
            var hashAlgorithm = new PasswordHashService();
            var salt = hashAlgorithm.GenerateSalt();
            var hash = hashAlgorithm.HashPassword(model.password, salt);
         
            
            
            var user = _userRepository.Create(model);
            _passwordHashRepository.Create(user.user_id, hash, salt);
            return user;
        }
        catch (Exception e)
        {
            throw new ValidationException("Error in register user");
        }
      
    }

    public UserModel Update(int user_id, UserModel userModel)
    {
        try
        {
            return _userRepository.Update(user_id,userModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            throw new ValidationException("Error in updating a user");
        }
    }
}