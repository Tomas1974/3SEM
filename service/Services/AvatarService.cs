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
    
    
    public IEnumerable<AvatarModel> GetAllAvatars()
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
    
    
    public AvatarModel CreateAvatar(AvatarModel avatar)
    {

        if (!ReferenceEquals(_avatarRepository.CheckIfNameExist(avatar.AvatarName), null))
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
        
    
    
    public AvatarModel UpdateAvatar(int avatarId, AvatarModel avatar)
    {
      
        try
        {
            return _avatarRepository.UpdateAvatar(avatarId,avatar);
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
