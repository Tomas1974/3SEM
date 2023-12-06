using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Logging;


namespace service.Services;

public class AccountService
{

    private readonly PasswordHashRepository _passwordHashRepository;
    private readonly UserRepository _userRepository;

    public AccountService(UserRepository userRepository,
        PasswordHashRepository passwordHashRepository)
    {

        _userRepository = userRepository;
        _passwordHashRepository = passwordHashRepository;
    }

    public User? Authenticate(LoginCommandModel model)
    {

        var passwordHash = _passwordHashRepository.GetByEmail(model.Email);

        if (HashPassword(model.Password, passwordHash.salt).SequenceEqual(passwordHash.hash))
            return _userRepository.GetById(passwordHash.user_id);
        else
            throw new ValidationException("Wrong Username or Password");

    }


    public User Register(RegisterCommandModel model)
    {
        var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
        var hash = HashPassword(model.password, salt);
        var user = _userRepository.Create(model);
        _passwordHashRepository.Create(user.user_id, hash, salt);
        return user;
    }

    public string HashPassword(string password, string salt)
        {
            using var hashAlgo = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = Convert.FromBase64String(salt),
                MemorySize = 12288, //Hukommelse der skal bruges i kb
                Iterations = 3, //
                DegreeOfParallelism = 1, // Antallet af kerner der bruges
            };
            return Convert.ToBase64String(hashAlgo.GetBytes(256));

        }

        public User GetAccountInfo()
        {
            return _userRepository.GetAccountInfo();

        }
    }
