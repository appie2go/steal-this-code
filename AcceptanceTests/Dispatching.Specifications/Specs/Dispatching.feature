Feature: Rides
	In order to save money
	As a dispatching business
	I want to dispatch cabs automatically

@e2e
Scenario: Available cab drives customer to the trainstation
	Given a cab
	  And a customer
	  And traffic information
	 When the customer has been driven to the trainstation
	 Then the cab it's new location is the trainstation
	  And the ride has been registered
