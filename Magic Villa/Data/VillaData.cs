using Magic_Villa.Models;

namespace Magic_Villa.Data
{
    public static class VillaData
    {
        public static List<VillaNumberDto> villaList = new List<VillaNumberDto>
        {
            new VillaNumberDto { Id = 1, Name = "Shubham" ,Sqft=300,Occupancy=2},
            new VillaNumberDto { Id = 2, Name = "Shubham23" ,Sqft=200,Occupancy=1}
        };
    }
}
