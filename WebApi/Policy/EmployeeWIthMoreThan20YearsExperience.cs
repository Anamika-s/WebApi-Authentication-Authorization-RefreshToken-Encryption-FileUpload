using Microsoft.AspNetCore.Authorization;

namespace WebApi.Policy
{
    public class EmployeeWIthMoreThan20YearsExperience : IAuthorizationRequirement
    {
        public int Years { get; set; }
        public EmployeeWIthMoreThan20YearsExperience(int years)
        {
            Years = years;
        }

        public EmployeeWIthMoreThan20YearsExperience()
        {
        }
    }
}
