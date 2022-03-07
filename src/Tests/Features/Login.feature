Feature: Login

As a user, I need to be able to log in to the system so that I can track my properties.

@BDD
Scenario: Existing user logging in
	Given I have registered for an account
		| FirstName | LastName | Email             | Username | Password  | ConfirmPassword | Gender | CountryofResidence | DateofBirth |
		| Test      | Name     | testing@gmail.com | testName | NotR3@Lpw | NotR3@Lpw       | Male   | United States      | 01/01/2000  |
	When I click the log in link
	And I submit the required information
		| Username | Password  |
		| testName | NotR3@Lpw |
	Then I have the ability to log in
