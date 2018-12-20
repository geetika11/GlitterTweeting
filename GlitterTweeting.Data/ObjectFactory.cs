using GlitterTweeting.Shared.DTO;
using System;

namespace GlitterTweeting.Data
{
    class ObjectFactory
    {
        public static User CreateNewUserObject(UserRegisterDTO user)
        {
            return new User
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Country = user.Country,
                PasswordHash = user.Password,
                ID = Guid.NewGuid()
            };
        }
    }

}
