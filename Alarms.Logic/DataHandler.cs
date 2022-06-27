using Alarms.Db.Entities;
using Alarms.Logic.DTO;
using Microsoft.EntityFrameworkCore;

namespace Alarms.Logic
{
    public class DataHandler
    {
        private AlarmsDbContext db;
        public DataHandler(AlarmsDbContext _db)
        {
            db = _db;

        }


        public IList<EventLogDto> SimpleDataFetch()
        {

            var dbData = db.EventLogs.
                Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State })
                .ToList(); ;



            return dbData;

        }
        public IList<EventLogDto> DataFetchWithDates(DateTime? startDate, DateTime? endDate, string? searchPhrase, string pSize, int pNumber)
        {
            List<EventLogDto> dbData = new List<EventLogDto>();

            var pSizeTemp = Convert.ToInt32(pSize);
            if (pNumber == 0)
            {
                pNumber = 1;
            }
            if (searchPhrase == null)
            {
                dbData = db.EventLogs.Where(x => (x.EventDateTime > startDate && x.EventDateTime < endDate))
                    .Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State })
                    .Skip(pSizeTemp * (pNumber - 1))
                    .Take(pSizeTemp)
                    .ToList();

            }
            else if (searchPhrase == null && startDate == null && endDate == null)
            {
                dbData = db.EventLogs
                    .Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State })
                    .Skip(pSizeTemp * (pNumber - 1))
                    .Take(pSizeTemp)
                    .ToList(); ;

            }
            else
            {
                dbData = db.EventLogs
                    .Where(x => (x.EventDateTime > startDate && x.EventDateTime < endDate) && x.TagData.TagName == searchPhrase ||
                    x.TagData.Description.Contains(searchPhrase))
                    .Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State })
                    .ToList(); ;
            }



            return dbData;

        }
        public QueryDto DataFetchRedesign(DateTime? startDate, DateTime? endDate, string? searchPhrase, string pSize, int pNumber, string? sortBy)
        {
            List<EventLogDto> dbData = new List<EventLogDto>();
            QueryDto queryDto = new QueryDto();

            var pSizeTemp = Convert.ToInt32(pSize);

            if (pNumber == 0)
            {
                pNumber = 1;
            }


            if (pSize == null)
            {
                pSizeTemp = 20;
            }

            var query = db.EventLogs
                .AsNoTracking()
                .AsQueryable();

            if (searchPhrase != null)
            {
                query = query.Where(r => r.TagData.Description
                .Contains(searchPhrase) || r.TagData.TagName == searchPhrase);
            }


            if (startDate != null)
            {
                query = query.Where(x => x.EventDateTime > startDate);
            }

            if (endDate != null)
            {
                query = query.Where(x => x.EventDateTime < endDate);
            }

            switch (sortBy)
            {
                case "TagName asc":
                    query = query.OrderBy(x => x.TagData.TagName);
                    break;
                case "TagName desc":
                    query = query.OrderByDescending(x => x.TagData.TagName);
                    break;
                case "EventDateTime asc":
                    query = query.OrderBy(x => x.EventDateTime);
                    break;
                case "EventDateTime desc":
                    query = query.OrderByDescending(x => x.EventDateTime);
                    break;
                case "TagDescription asc":
                    query = query.OrderBy(x => x.TagData.Description);
                    break;
                case "TagDescription desc":
                    query = query.OrderByDescending(x => x.TagData.Description);
                    break;
                case "State asc":
                    query = query.OrderBy(x => x.State);
                    break;
                case "State desc":
                    query = query.OrderByDescending(x => x.State);
                    break;


            }
            //Total number of pages calculation.
            var total = query.ToList().Count();
            queryDto.PageCount = total / pSizeTemp;
            var remainder = 0;
            Math.DivRem(total, pSizeTemp, out remainder);

            if (remainder > 0)
            {
                queryDto.PageCount = queryDto.PageCount + 1;
            }

            if (pNumber > 1)
            {
                query = query.Skip((pNumber - 1) * pSizeTemp);
            }

            if (pSizeTemp > 0)
            {
                query = query.Take(pSizeTemp);
            }


            var dbData2 = query.Select(r =>
                new EventLogDto
                {
                    EventDateTime = r.EventDateTime,
                    TagName = r.TagData.TagName,
                    State = r.State,
                    TagDescription = r.TagData.Description
                })
                    .ToList();

            queryDto.CurrentPage = pNumber;
            queryDto.sortBy = sortBy;
            queryDto.sortedBy = sortBy;
            queryDto.ItemPerPage = pSizeTemp;
            queryDto.events = dbData2;



            return queryDto;

        }
    }
}
