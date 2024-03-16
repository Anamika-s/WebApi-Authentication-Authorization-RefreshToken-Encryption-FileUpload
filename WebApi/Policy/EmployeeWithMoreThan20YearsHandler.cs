using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Policy
{
    public class A : AuthorizationHandler<EmployeeWIthMoreThan20YearsExperience>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeWIthMoreThan20YearsExperience requirement)
        {
            throw new NotImplementedException();
        }
    }
    public class EmployeeWithMoreThan20YearsHandler : AuthorizationHandler<EmployeeWIthMoreThan20YearsExperience>
    {
        EmployeeNumberofYears _emp;
        public EmployeeWithMoreThan20YearsHandler(EmployeeNumberofYears emp)
        {
            _emp = emp;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            EmployeeWIthMoreThan20YearsExperience requirement
           )
        {
            
            var years = _emp.NumberOfyears();
            if (years >= requirement.Years)



            {
                context.Succeed(requirement);
            }
                return Task.FromResult(0);
            
        }
    }
}
