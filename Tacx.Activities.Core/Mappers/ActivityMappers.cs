using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Core.Mappers
{
    public static class ActivityMappers
    {
        public static Activity ToEntity(this ActivityDto dto)
            => new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Duration = dto.Duration,
                Distance = dto.Distance,
                AvgSpeed = dto.AvgSpeed,
            };

        public static ActivityDto ToDto(this Activity activity)
            => new()
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Duration = activity.Duration,
                Distance = activity.Distance,
                AvgSpeed = activity.AvgSpeed,
            };
    }
}
