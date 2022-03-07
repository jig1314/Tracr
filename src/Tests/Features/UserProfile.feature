Feature: User Profile

As a user, I need to be able view my user profile page so that I can see all of my information.

@BDD
Scenario: Accessing user profile
	Given I have registered for an account
		| FirstName | LastName | Email             | Username | Password  | ConfirmPassword | Gender | CountryofResidence | DateofBirth |
		| Test      | Name     | testing@gmail.com | testName | NotR3@Lpw | NotR3@Lpw       | Male   | United States      | 01/01/2000  |
	When I click the view profile link
	Then I have the ability to view user profile
