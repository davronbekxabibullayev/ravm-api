namespace Ravm.Infrastructure.Extensions.DataSeeding;

using System.Data;
using Ravm.Application.Common;
using Ravm.Domain.Models;

public static class DataSeeder
{
    public static List<Country> ReadCountriesFromExcel(this DataTable dataTable)
    {
        var result = new List<Country>();
        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var mvdId = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(mvdId))
                result.Add(new Country()
                {
                    Code = cells[0]!.ToString()!.Trim(),
                    StateCode = cells[1]!.ToString()!.Trim(),
                    Name = cells[2]!.ToString()!.Trim(),
                    NameRu = cells[3]!.ToString()!.Trim(),
                    NameUz = cells[4]?.ToString()?.Trim(),
                    NameKa = cells[5]?.ToString()?.Trim()
                });
        }
        return result;
    }

    public static List<Region> ReadRegionsFromExcel(this DataTable dataTable, IAppDbContext dbContext)
    {
        var result = new List<Region>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var countryCode = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(countryCode))
            {
                var country = dbContext.Countries.FirstOrDefault(c =>
                c.Code == countryCode);
                result.Add(new Region()
                {
                    CountryId = country!.Id,
                    Code = cells[1]!.ToString()!,
                    StateCode = cells[2]!.ToString(),
                    Name = cells[3]?.ToString()!,
                    NameRu = cells[4]?.ToString()!,
                    NameUz = cells[5]?.ToString(),
                    NameKa = cells[6]?.ToString(),
                });
            }
        }
        return result;
    }

    public static List<City> ReadCitiesFromExcel(this DataTable dataTable, IAppDbContext dbContext)
    {
        var result = new List<City>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var regionCode = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(regionCode))
            {
                var region = dbContext.Regions.FirstOrDefault(c =>
                c.Code == regionCode.ToString()!);
                result.Add(new City()
                {
                    RegionId = region!.Id,
                    Code = cells[1]!.ToString()!,
                    StateCode = cells[2]?.ToString(),
                    Name = cells[3]?.ToString()!,
                    NameRu = cells[4]?.ToString()!,
                    NameUz = cells[5]?.ToString(),
                    NameKa = cells[6]?.ToString(),
                });
            }
        }
        return result;
    }

    public static List<Occupation> ReadOccupationsFromExcel(this DataTable dataTable)
    {
        var result = new List<Occupation>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var code = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(code))
                result.Add(new Occupation
                {
                    Code = cells[0]!.ToString()!,
                    Name = cells[1]!.ToString()!,
                    NameRu = cells[2]!.ToString()!,
                    NameUz = cells[3]!.ToString(),
                    NameKa = cells[4]!.ToString(),
                });
        }
        return result;
    }

    public static List<Specialization> ReadSpecializationsFromExcel(this DataTable dataTable)
    {
        var result = new List<Specialization>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var code = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(code))
                result.Add(new Specialization
                {
                    Code = cells[0]!.ToString()!,
                    Name = cells[1]?.ToString()!,
                    NameRu = cells[2]?.ToString()!,
                    NameUz = cells[3]?.ToString(),
                    NameKa = cells[4]?.ToString(),
                });
        }

        return result;
    }

    public static List<VehicleMark> ReadVehicleMarksFromExcel(this DataTable dataTable)
    {
        var result = new List<VehicleMark>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var code = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(code))
                result.Add(new VehicleMark
                {
                    Code = cells[0]!.ToString()!,
                    Name = cells[1]?.ToString()!,
                    NameRu = cells[2]?.ToString()!,
                    NameUz = cells[3]?.ToString(),
                    NameKa = cells[4]?.ToString(),
                });
        }
        return result;
    }

    public static List<VehicleModel> ReadVehicleModelsFromExcel(this DataTable dataTable, IAppDbContext dbContext)
    {
        var result = new List<VehicleModel>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var vmCode = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(vmCode))
            {
                var vehicleMark = dbContext.VehicleMarks.FirstOrDefault(c =>
                c.Code == vmCode);
                result.Add(new VehicleModel
                {
                    VehicleMarkId = vehicleMark!.Id,
                    Code = cells[1]!.ToString()!,
                    Name = cells[2]!.ToString()!,
                    NameRu = cells[3]!.ToString()!,
                    NameUz = cells[4]!.ToString(),
                    NameKa = cells[5]!.ToString(),
                });
            }
        }
        return result;
    }

    public static List<Reason> ReadReasonsFromExcel(this DataTable dataTable)
    {
        var result = new List<Reason>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var code = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(code))
                result.Add(new Reason()
                {
                    Code = cells[0]!.ToString()!,
                    Name = cells[1]!.ToString()!,
                    NameRu = cells[2]!.ToString()!,
                    NameUz = cells[3]!.ToString(),
                    NameKa = cells[4]!.ToString()
                });
        }
        return result;
    }

    public static List<RouteClassification> ReadRouteClassificationsFromExcel(this DataTable dataTable)
    {
        var result = new List<RouteClassification>();
        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var code = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(code))
                result.Add(new RouteClassification
                {
                    Code = cells[0]!.ToString()!,
                    Name = cells[1]!.ToString()!,
                    NameRu = cells[2]!.ToString()!,
                    NameUz = cells[3]!.ToString(),
                    NameKa = cells[4]!.ToString()
                });
        }
        return result;
    }

    public static List<StopPoint> ReadStopPointsFromExcel(this DataTable dataTable)
    {
        var result = new List<StopPoint>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var code = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(code))
                result.Add(new StopPoint
                {
                    Code = cells[0]!.ToString()!,
                    Position = (Domain.Enums.StopPointPosition)int.Parse(cells[1].ToString()),
                    Name = cells[2]?.ToString()!,
                    NameRu = cells[3]?.ToString()!,
                    NameUz = cells[4]?.ToString(),
                    NameKa = cells[5]?.ToString(),
                });
        }
        return result;
    }
}
