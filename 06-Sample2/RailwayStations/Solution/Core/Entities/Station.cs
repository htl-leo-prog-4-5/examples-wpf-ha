using Base.Core.Entities;

namespace Core.Entities;

public class Station : EntityObject
{
    public          string? Code { get; set; }
    public required string  Name { get; set; }

    public          string? Type      { get; set; }
    public required string  StateCode { get; set; }

    public bool    IsRegional  { get; set; }
    public bool    IsExpress   { get; set; }
    public bool    IsIntercity { get; set; }
    public string? Remark      { get; set; }


    public int   CityId { get; set; }
    public City? City   { get; set; }


    public IList<Infrastructure>? Infrastructures  { get; set; }
    public IList<RailwayCompany>? RailwayCompanies { get; set; }
    public IList<Line>?           Lines            { get; set; }
}