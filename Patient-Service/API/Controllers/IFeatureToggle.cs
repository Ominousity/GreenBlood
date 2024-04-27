namespace API.Controllers
{
    public interface IFeatureToggle
    {
        public Task<bool> CanDoctorDelete();
        public Task<bool> CanDoctorGet();
        public Task<bool> IsFromDenmark(string Country);
    }
}
