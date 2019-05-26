Feature: Age calculation

Background: 
	Given a date time calculations provider

@Ignore
Scenario: Same date the very next month
	Given a kid was born on "2016/01/10"
	When their age is calculated on "2016/02/10"
	Then the age should be 0 years 1 months 0 days

@Ignore
Scenario: Same date the very next year
	Given a kid was born on "2016/01/10"
	When their age is calculated on "2017/01/10"
	Then the age should be 1 years 0 months 0 days

@Ignore
Scenario: A few days in the same month
	Given a kid was born on "2016/01/10"
	When their age is calculated on "2016/01/20"
	Then the age should be 0 years 0 months 10 days

@Ignore
Scenario: At the first of the next month
	Given a kid was born on "2016/01/20"
	When their age is calculated on "2016/02/01"
	Then the age should be 0 years 0 months 12 days
	
@Ignore
Scenario: At the first of the next month after a 31-days month
	Given a kid was born on "2016/11/20"
	When their age is calculated on "2016/12/01"
	Then the age should be 0 years 0 months 11 days
	
@Ignore
Scenario: Exact date two months later in the same year
	Given a kid was born on "2016/03/05"
	When their age is calculated on "2016/05/05"
	Then the age should be 0 years 2 months 0 days

@Ignore
Scenario: Exact date two months later in the next year
	Given a kid was born on "2015/12/10"
	When their age is calculated on "2016/01/10"
	Then the age should be 0 years 2 months 0 days

@Ignore
Scenario: February 29 and same date the very next month in the same year
	Given a kid was born on "2016/02/29"
	When their age is calculated on "2016/03/29"
	Then the age should be 0 years 1 months 0 days

@Ignore
Scenario: Leapers have birthays the date after February 28 in normal years
	Given a kid was born on "2016/02/29"
	When their age is calculated on "2019/02/28"
	Then the age should be 2 years 11 months 28 days

@Ignore
Scenario: Leapers have birthays on March 1st in normal years
	Given a kid was born on "2016/02/29"
	When their age is calculated on "2019/03/01"
	Then the age should be 3 years 0 months 0 days

@Ignore
Scenario: Leapers have birthays on February 29th in leap years
	Given a kid was born on "2016/02/29"
	When their age is calculated on "2020/02/29"
	Then the age should be 4 years 0 months 0 days

@Ignore
Scenario: Same date the very next month for February on leap years
	Given a kid was born on "2016/02/1"
	When their age is calculated on "2016/03/01"
	Then the age should be 0 years 1 months 0 days

@Ignore
Scenario: Same date the very next month for February on normal years
	Given a kid was born on "2017/02/1"
	When their age is calculated on "2017/03/01"
	Then the age should be 0 years 1 months 0 days