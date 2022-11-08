using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TestWork.Data;
using TestWork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestWork.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly TestWorkContext _context;
        public IncidentsController(TestWorkContext context)
        {
            _context = context;
        }
        public IList<Contact> contacts { get; set; }
        public IList<Account> accounts { get; set; }
        public IList<Incident> incidents { get; set; }
        /// <summary>
        /// Returns a contact for a given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<ActionResult> GetContact(string email)
        {
            contacts = await _context.Contacts.ToListAsync();
            foreach (var item in contacts)
            {
                if (item.Email == email)
                    return Ok(item);
            }
            return NotFound();
        }
        /// <summary>
        /// Returns a account for a given account_name
        /// </summary>
        /// <param name="account_name"></param>
        /// <returns></returns>
        [HttpGet("{account_name}")]
        public async Task<ActionResult> GetAccount(string account_name)
        {
            accounts = await _context.Accounts.ToListAsync();
            foreach (var item in accounts)
            {
                if (item.AccountName == account_name)
                    return Ok(item);
            }
            return NotFound();
        }
        /// <summary>
        /// Returns a incident for a given incident_name
        /// </summary>
        /// <param name="incident_name"></param>
        /// <returns></returns>
        [HttpGet("{incident_name}")]
        public async Task<ActionResult> GetIncident(string incident_name)
        {
            incidents = await _context.Incidents.ToListAsync();
            foreach (var item in incidents)
            {
                if (item.IncidentName == incident_name)
                    return Ok(item);
            }
            return NotFound();
        }
        /// <summary>
        /// Update a contact for a given email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="contactUpdates"></param>
        /// <returns></returns>
        [HttpPatch("{email}")]
        public async Task<ActionResult> UpdateContact(string email,[FromBody]JsonPatchDocument<Contact> contactUpdates)
        {
            contacts=await _context.Contacts.ToListAsync();
            foreach (var item in contacts)
            {
                if(item.Email==email)
                {
                    if (item == null)
                        return NotFound();
                    contactUpdates.ApplyTo(item);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            return NotFound();
        }
        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody]Contact contact)
        {
           if(contact == null)
                return BadRequest();
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContact),new { email = contact.Email }, contact);
        }
        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody]Account account)
        {
            if (account == null)
                return BadRequest();
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccount), new { account_name = account.AccountName }, account);
        }
        /// <summary>
        /// Create Incident
        /// </summary>
        /// <param name="incident"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateIncident([FromBody] Incident incident)
        {
            if (incident == null)
                return BadRequest();
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetIncident), new { incident_name = incident.IncidentName }, incident);
        }

    }
}
