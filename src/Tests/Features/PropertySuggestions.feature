Feature: Property Suggestions

As a user, I need to be able to receive suggestions for properties based on my projected profits so that I can make future investment decisions.

@BDD
Scenario: Property suggestions with zero projected profits 
	Given I have registered for an account
		| FirstName | LastName | Email             | Username | Password  | ConfirmPassword | Gender | CountryofResidence | DateofBirth |
		| Test      | Name     | testing@gmail.com | testName | NotR3@Lpw | NotR3@Lpw       | Male   | United States      | 01/01/2000  |
	When I click the suggestions button
	Then I will not receive property suggestions