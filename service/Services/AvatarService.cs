using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class AvatarService
{
    private readonly AvatarRepository _avatarRepository;
    
    public AvatarService(AvatarRepository avatarRepository)
    {
        _avatarRepository = avatarRepository;
    }
    
    
    public IEnumerable<Avatar> GetAllAvatars()
    {
        try
        {
            return _avatarRepository.GetAllAvatars();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting all  Avatars");
        }
        
        
       
    }
    
    
    public Avatar CreateAvatar(Avatar avatar)
    {

        if (!ReferenceEquals(_avatarRepository.CheckIfNameExist(avatar.avatar_name), null))
            throw new ValidationException("Already exists");
        
        try
            {
                return _avatarRepository.CreateAvatar(avatar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ValidationException("Error in creating an Avatar");
            }
        }
        
    
    
    public Avatar UpdateAvatar(int avatar_id, Avatar avatar)
    {
      
        try
        {
            return _avatarRepository.UpdateAvatar(avatar_id,avatar);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in updating an Avatar");
        }
        
        
    }
    
    public void deleteAvatar(int avatar_id)
    {
        try
        {
             _avatarRepository.DeleteAvatar(avatar_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in deleting an Avatar");
        }
    }

    public string GetAvatarInformation(int avatar_id)
    {
        try
        {
            return _avatarRepository.GetAvatarInformation(avatar_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting all avatar information");
        }
    }
}
