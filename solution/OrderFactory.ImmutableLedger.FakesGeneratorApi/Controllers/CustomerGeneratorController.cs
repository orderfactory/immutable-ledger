using Faker;
using Microsoft.AspNetCore.Mvc;
using OrderFactory.GuidGenerator;
using OrderFactory.ImmutableLedger.Business;
using OrderFactory.ImmutableLedger.Repo;

namespace OrderFactory.ImmutableLedger.FakesGeneratorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerGeneratorController(IGuidGenerator guidGenerator, IRepository repository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GenerateCustomer()
    {
        var middleName = RandomNumber.Next(3) > 1 ? Name.Middle() : null;
        var customer = new Customer(
            guidGenerator.NewGuid(),
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            null,
            Name.First(),
            middleName,
            Name.Last()
        );
        var count = await repository.SaveCustomer(customer);
        if (count > 0) return Ok(customer);

        return BadRequest();
    }
}