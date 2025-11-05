using Microsoft.IdentityModel.Tokens;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Services.DTO.ProfileDTO;
using NextUse.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        //private ProfileResponse MapProfileToProfileResponse(Profile profile)
        //{
        //    var profileResponse = new ProfileResponse
        //    {
        //        Id = profile.Id,
        //        Name = profile.Name,
        //        AverageRating = profile.Ratings.IsNullOrEmpty() ? 0 : profile.Ratings!.Where(r => r.ToProfileId == profile.Id).Average(r => r.Score),
        //        RatingAmount = profile.Ratings.Count(),
        //        Address = new ProfileAddressResponse
        //        {
        //            Id = profile.Address!.Id,
        //            Country = profile.Address!.Country,
        //            City = profile.Address.City,
        //            PostalCode = profile.Address.PostalCode,
        //            Street = profile.Address.Street,
        //            Housenumber = profile.Address.HouseNumber
        //        },
        //        Products = profile.Products is null ? [] : profile.Products.Select(product => new ProfileProductResponse
        //        {
        //            Id = product.Id,
        //            Title = product.Title,
        //            Description = product.Description,
        //            Price = product.Price,
        //        })
        //    };

        //    if (!profile.Bookmarks.IsNullOrEmpty())
        //    {
        //        profileResponse.Bookmarks = profile.Bookmarks?.Select(bookmark => new ProfileBookmarkResponse
        //        {
        //            Id = bookmark.Id,
        //            Product = bookmark.Product is null ? null : new BookmarkProductResponse
        //            {
        //                Id = bookmark.Product.Id,
        //                Title = bookmark.Product.Title,
        //                Description = bookmark.Product.Description,
        //                Price = bookmark.Product.Price,
        //            }
        //        });
        //    }

        //    return profileResponse;
        //}

        private ProfileResponse MapProfileToProfileResponse(Profile profile)
        {
            var profileResponse = new ProfileResponse
            {
                Id = profile.Id,
                Name = profile.Name,
                AverageRating = (profile.Ratings == null || !profile.Ratings.Any())
                    ? 0
                    : profile.Ratings.Where(r => r.ToProfileId == profile.Id).Average(r => r.Score),
                RatingAmount = profile.Ratings?.Count() ?? 0,
                Address = new ProfileAddressResponse
                {
                    Id = profile.Address!.Id,
                    Country = profile.Address.Country,
                    City = profile.Address.City,
                    PostalCode = profile.Address.PostalCode,
                    Street = profile.Address.Street,
                    Housenumber = profile.Address.HouseNumber
                },
                Products = profile.Products == null
                    ? []
                    : profile.Products.Select(product => new ProfileProductResponse
                    {
                        Id = product.Id,
                        Title = product.Title,
                        Description = product.Description,
                        Price = product.Price,
                    })
            };

            if (profile.Bookmarks != null && profile.Bookmarks.Any())
            {
                profileResponse.Bookmarks = profile.Bookmarks.Select(bookmark => new ProfileBookmarkResponse
                {
                    Id = bookmark.Id,
                    Product = bookmark.Product == null ? null : new BookmarkProductResponse
                    {
                        Id = bookmark.Product.Id,
                        Title = bookmark.Product.Title,
                        Description = bookmark.Product.Description,
                        Price = bookmark.Product.Price,
                    }
                });
            }

            return profileResponse;
        }


        private Profile MapProfileRequestToProfile(ProfileRequest profileRequest)
        {
            return new Profile
            {
                Name = profileRequest.Name,
                AddressId = profileRequest.AddressId,
                UserId = profileRequest.UserId,
            };
        }

        public async Task<IEnumerable<ProfileResponse>> GetAllAsync()
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetAllAsync();

            return profiles.Select(MapProfileToProfileResponse).ToList();
        }

        public async Task<ProfileResponse?> GetByIdAsync(int id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);

            return profile is null ? null : MapProfileToProfileResponse(profile);
        }

        public async Task<ProfileResponse?> GetByUserIdAsync(string userId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);

            return profile is null ? null : MapProfileToProfileResponse(profile);
        }

        public async Task<ProfileResponse> AddAsync(ProfileRequest newProfileRequest)
        {
            var profile = MapProfileRequestToProfile(newProfileRequest);

            var insertedProfile = await _profileRepository.AddAsync(profile);

            return MapProfileToProfileResponse(insertedProfile);
        }
        public async Task<ProfileResponse> UpdateByIdAsync(int id, ProfileRequest updatedProfileRequest)
        {
            var profile = MapProfileRequestToProfile(updatedProfileRequest);
            var updatedProfile = await _profileRepository.UpdateByIdAsync(id, profile);

            return MapProfileToProfileResponse(updatedProfile);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _profileRepository.DeleteByIdAsync(id);
        }

        public async Task<bool> BlockByIdAsync(int profileId)
        {
            return await _profileRepository.BlockByIdAsync(profileId);
        }

        public async Task<bool> UnblockByIdAsync(int profileId)
        {
            return await _profileRepository.UnblockByIdAsync(profileId);
        }

    }
}
