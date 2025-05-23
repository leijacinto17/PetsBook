﻿using System.Diagnostics;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace PetsBook.API.Extensions.DataInitializer
{
    public class DataInitializer
    {
        public async static Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            //check dev user if existing
            var devUser = await userManager.FindByEmailAsync("devuser@development.com");
            if (devUser == null)
            {
                try
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "devuser@development.com",
                        Email = "devuser@development.com",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "dev@userP@ssw0rd");

                    if (result.Succeeded)
                    {
                        Debug.WriteLine("User created successfully");
                    }
                    else
                    {
                        Debug.WriteLine("Error creating user");
                        foreach (var error in result.Errors)
                        {
                            Debug.WriteLine($" - {error}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
