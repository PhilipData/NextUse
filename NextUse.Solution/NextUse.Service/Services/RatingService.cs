using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Service.DTO.RatingDTO;
using NextUse.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Service.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        private RatingResponse MapRatingToRatingResponse(Rating rating)
        {
            var copenhagenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var createdAtCopenhagen = TimeZoneInfo.ConvertTimeToUtc(rating.CreatedAt, copenhagenTimeZone);
            var ratingResponse = new RatingResponse
            {
                Id = rating.Id,
                Score = rating.Score,
                CreatedAt = createdAtCopenhagen.ToString("yyyy-MM-dd HH:mm:ss"), //Formatted for readability
                ToProfile = new RatingProfileResponse
                {
                    Id = rating.ToProfile!.Id,
                    Name = rating.ToProfile.Name
                }
            };

            if (rating.FromProfile != null)
            {
                ratingResponse.FromProfile = new RatingProfileResponse
                {
                    Id = rating.FromProfile!.Id,
                    Name = rating.FromProfile.Name
                };
            }

            return ratingResponse;
        }

        private Rating MapRatingRequestToRating(RatingRequest ratingRequest)
        {
            return new Rating
            {
                Score = ratingRequest.Score,
                FromProfileId = ratingRequest.FromProfileId,
                ToProfileId = ratingRequest.ToProfileId
            };
        }
        public async Task<IEnumerable<RatingResponse>> GetAllAsync()
        {
            IEnumerable<Rating> ratings = await _ratingRepository.GetAllAsync();

            return ratings.Select(MapRatingToRatingResponse).ToList();
        }

        public async Task<RatingResponse?> GetByIdAsync(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);

            return rating is null ? null : MapRatingToRatingResponse(rating);
        }

        public async Task<RatingResponse> AddAsync(RatingRequest newRatingRequest)
        {
            if (newRatingRequest.FromProfileId.Equals(newRatingRequest.ToProfileId))
                throw new InvalidOperationException("FromProfileId and ToProfileId must not be equal");

            if (await _ratingRepository.AlreadyRated(newRatingRequest.FromProfileId, newRatingRequest.ToProfileId))
                throw new InvalidOperationException("A rating already exists for the given profile");

            var rating = MapRatingRequestToRating(newRatingRequest);

            var insertedRating = await _ratingRepository.AddAsync(rating);

            return MapRatingToRatingResponse(insertedRating);
        }
        public async Task<RatingResponse> UpdateByIdAsync(int id, RatingRequest updatedRatingRequest)
        {
            var rating = MapRatingRequestToRating(updatedRatingRequest);

            var updatedRating = await _ratingRepository.UpdateByIdAsync(id, rating);

            return MapRatingToRatingResponse(updatedRating);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _ratingRepository.DeleteByIdAsync(id);
        }


        

    }
}
