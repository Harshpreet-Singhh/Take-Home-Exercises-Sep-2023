<Query Kind="Program">
  <Connection>
    <ID>58a75dc3-4056-40f8-887b-7389cf3ee121</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>LIBSHORT-206\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>OLTP-20231025</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//  YOUR NAME HERE:Harshpreet Singh

	#region Driver  //  3 Marks
	try
	{
		//  The driver must, at minimum perform three different task. 
		//  Task 1
		//  -   Add a new employee and register their skills (minimun of two skills). 
		// Task 1: Add a new employee and register their skills
		var newEmployee = new EmployeeRegistrationView
		{
			FirstName = "Butta",
			LastName = "Singh",
			HomePhone = RandomPhoneNumber(),
			Active = true,
			EmployeeSkills = new List<EmployeeSkillView>
	{
		new EmployeeSkillView
		{
			SkillID = 7,
			Level = 8,
			YearsOfExperience = 1,
			HourlyWage = 20
		},
		new EmployeeSkillView
		{
			SkillID = 2,
			Level = 2,
			YearsOfExperience = 1,
			HourlyWage = 28
		}
	}
		};

		AddEditEmployeeRegistration(newEmployee);

		//		
		//  Task 2 update an employee and their skill list. 
		//  -   Updating their first or last name
		//  -   Updating one existing skill
		//  -   adding a minimum of one new skill
		var employeeToUpdate = GetEmployeeRegistration(3); // Replace employeeId with the actual ID

		if (employeeToUpdate != null)
		{
			// Task: Updating the employee's first or last name
			// Example: Updating the first name
			employeeToUpdate.FirstName = "New First Name";

			// Task: Updating an existing skill
			if (employeeToUpdate.EmployeeSkills.Count > 0)
			{
				employeeToUpdate.EmployeeSkills[0].Level = 5;
			}
			employeeToUpdate.EmployeeSkills.Add(new EmployeeSkillView
			{
				SkillID = 3,
				Level = 2,
				YearsOfExperience = 1,
				HourlyWage = 18.0m
			});
			AddEditEmployeeRegistration(employeeToUpdate);
		}

		//  Task 3 attempts to register new skills with invalid data that will trigger all the business in this exercise
		//  Refer to business rules for all test cases
	}
	#endregion

	#region catch all exceptions 
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}
		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
		#endregion
		}
		private Exception GetInnerException(Exception ex)
		{
			while (ex.InnerException != null)
				ex = ex.InnerException;
			return ex;
		}



#region Methods

#region AddEditEmployeeRegistration Method   //  6 Marks
public EmployeeRegistrationView AddEditEmployeeRegistration(EmployeeRegistrationView employeeRegistration)
{
    try
    {
        // --- Business Logic and Parameter Exception Section --- 
        #region Business Logic and Parameter Exception  //  2 Marks

        if (employeeRegistration == null)
        {
            throw new ArgumentNullException(nameof(employeeRegistration), "Employee details cannot be empty.");
        }

		if (string.IsNullOrEmpty(employeeRegistration.FirstName) || string.IsNullOrEmpty(employeeRegistration.LastName))
		{
			throw new ArgumentException("First name and last name is required.", nameof(employeeRegistration));
		}
		if (employeeRegistration.EmployeeSkills.Count < 2)
		{
		    throw new ArgumentException("Mention two skills.", nameof(employeeRegistration.EmployeeSkills));
		}

		#endregion

		// --- Main Method Logic Section --- 
		#region Method Code //  3 Marks
		
		if (employeeRegistration.EmployeeID > 0)
		{
			// Employees.UpdateEmployee(employeeRegistration);
		}
		else
		{
			// Employees.AddEmployee(employeeRegistration);
		}

		#endregion

		#region Check for errors and saving of data //  1 Marks

		//Employees.SaveChanges(); 

		#endregion

		return employeeRegistration;
	}
	catch (Exception ex)
	{
		throw; 
	}
}
#endregion

#region GetEmployeeRegistration Method    //  1 Marks
public EmployeeRegistrationView GetEmployeeRegistration(int employeeId)
{
	try
	{
		EmployeeRegistrationView employee = ;

		if (employee == null)
		{
			throw new ArgumentException("employee not found", nameof(employeeId));
		}
		return employee;
	}
	catch (Exception ex)
	{
		throw; 
	}
}

#endregion


#endregion

/// <summary> 
/// Contains class definitions that are referenced in the current LINQ file. 
/// </summary> 
/// <remarks> 
/// It's crucial to highlight that in standard development practices, code and class definitions  
/// should not be mixed in the same file. Proper separation of concerns dictates that classes  
/// should have their own dedicated files, promoting modularity and maintainability. 
/// </remarks> 
#region Class/View Model   

public class EmployeeRegistrationView
{
	public int EmployeeID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string HomePhone { get; set; }
	public bool Active { get; set; }
	public List<EmployeeSkillView> EmployeeSkills { get; set; } = new();
}

public class EmployeeSkillView
{
	public int EmployeeSkillID { get; set; }
	public int EmployeeID { get; set; }
	public int SkillID { get; set; }
	public int Level { get; set; }
	public int? YearsOfExperience { get; set; }
	public decimal HourlyWage { get; set; }
}

#endregion

#region Supporting Methods
/// <summary>
/// Generates a random phone number.
/// The generated phone number ensures the first digit is not 0 or 1.
/// </summary>
/// <returns>A random phone number.</returns>
public static string RandomPhoneNumber()
{
	var random = new Random();
	string phoneNumber = string.Empty;

	// Ensure the first digit isn't 0 or 1.
	int firstDigit = random.Next(2, 10); // Generates a random digit between 2 and 9.
	phoneNumber = $"{firstDigit}";

	// Generate the rest of the digits.
	for (int i = 1; i < 10; i++)
	{
		int currentDigit = random.Next(10);
		phoneNumber = $"{phoneNumber}{currentDigit}";

		// Add periods after every third digit (except for the last period).
		if (i % 3 == 2 && i != 8)
		{
			phoneNumber = $"{phoneNumber}.";
		}
	}

	return phoneNumber;
}

/// <summary>
/// Generates a random name of a given length.
/// The generated name follows a pattern of alternating consonants and vowels.
/// </summary>
/// <param name="len">The desired length of the generated name.</param>
/// <returns>A random name of the specified length.</returns>
public static string GenerateName(int len)
{
	// Create a new Random instance.
	Random r = new Random();

	// Define consonants and vowels to use in the name generation.
	string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
	string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };

	string Name = "";

	// Start the name with an uppercase consonant and a vowel.
	Name += consonants[r.Next(consonants.Length)].ToUpper();
	Name += vowels[r.Next(vowels.Length)];

	// Counter for tracking the number of characters added.
	int b = 2;

	// Add alternating consonants and vowels until we reach the desired length.
	while (b < len)
	{
		Name += consonants[r.Next(consonants.Length)];
		b++;
		Name += vowels[r.Next(vowels.Length)];
		b++;
	}

	return Name;
}
#endregion