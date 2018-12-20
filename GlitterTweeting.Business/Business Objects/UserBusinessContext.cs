﻿using AutoMapper;
using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared;
using GlitterTweeting.Shared.DTO;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Business_Objects
{
   
        public class UserBusinessContext : IDisposable
        {
            private UserDBContext UserDBContext;
            private IMapper UserMapper;


            /// <summary>
            /// Constructor, initializes DB context objects and automappers.
            /// </summary>
            public UserBusinessContext()
            {
                UserDBContext = new UserDBContext();
                var userMappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserRegisterDTO, UserCompleteDTO>();
                });
                UserMapper = new Mapper(userMappingConfig);
            }

            /// <summary>
            /// Creates and returns new user
            /// </summary>
            /// <param name="userInput">User info</param>
            /// <returns>created user or exception</returns>
            public async Task<UserCompleteDTO> CreateNewUser(UserRegisterDTO userInput)
            {

                if (UserDBContext.EmailExists(userInput.Email))
                {
                    userInput.Password = PasswordHasher.HashPassword(userInput.Password);
                    if (userInput.Image == null)
                    {
                        userInput.Image = Constants.DEFAULT_USER_IMAGE;
                    }
                    Guid ID = await UserDBContext.CreateNewUser(userInput);

                    UserCompleteDTO user;
                    user = UserMapper.Map<UserRegisterDTO, UserCompleteDTO>(userInput);
                    user.ID = ID;
                    return user;
                }
                else
                {
                    throw new Exceptions.AlreadyExistsException("Email address already in use");
                }


            }

            /// <summary>
            /// Checks if the login is valid.
            /// </summary>
            /// <param name="email">Email of the user</param>
            /// <param name="password">Password of the user</param>
            /// <returns>ID of the user or exception</returns>


            /// <summary>
            /// Returns requested user
            /// </summary>
            /// <param name="id">ID of the requested user</param>
            /// <returns>Requested user information</returns>

            /// <summary>
            /// Dispose to class
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// virtual dispose to class
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (UserDBContext != null)
                    {
                        UserDBContext.Dispose();
                    }
                }
            }

            /// <summary>
            /// Destructor to class
            /// </summary>
            ~UserBusinessContext()
            {
                Dispose(false);
            }

        }

        }
    