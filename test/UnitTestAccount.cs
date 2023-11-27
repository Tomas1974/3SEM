using System.Net;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

[TestFixture]
public class Tests
{
    
    [TestCase("Bo", "Bogade 1", 1000, "bob@cool.com", "123456789" )]
    [TestCase("Ib","Bogade 2", 1000, "Ib@cool.com", "123456789" )]
    public async Task UserCanSuccessfullyBeCreated(string full_name, string street, int zip, string email, string password)
    {
        Helper.TriggerRebuild();
        var testUser = new User
        {
            full_name = full_name,
            street = street,
            zip = zip,
            email = email,
            password = password
        };
        
        var httpResponse  =await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<User>>(responseBodyString);

        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.email.Should().BeEquivalentTo(testUser.email);
        }
    }
    
    [TestCase("t", "Bogade 1", 1000, "bobyugbhnj@cool.com", "123456789" )]
    [TestCase("Birgitte", "j", 1000, "bobyugbhnj@cool.com", "123456789" )]
    [TestCase("Benter", "Bobogade 2", 1000, "cool.com", "123456789" )]

    public async Task CanNotCreateUserWithInvalidCharacter(string full_name, string street, int zip, string email, string password)
    {
        Helper.TriggerRebuild();
        var testUser = new User
        {
            full_name = full_name,
            street = street,
            zip = zip,
            email = email,
            password = password
        };
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
       
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            
           httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
    
    //Email not the same
}