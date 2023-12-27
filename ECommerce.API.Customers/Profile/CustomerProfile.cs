namespace ECommerce.API.Customers.Profile
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<DB.Customer, Models.Customer>();
        }
    }
}
