<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
<h1>API Documentation</h1>

<h2>GET /persons</h2>
<pre>
  [
  {
    "personId": 1,
    "firstName": "Jana",
    "lastName": "Johansson",
    "phoneNumber": 456985312
  },
  {
    "personId": 2,
    "firstName": "Olle",
    "lastName": "Ottosson",
    "phoneNumber": 56988744
  },
  {
    "personId": 3,
    "firstName": "Marc",
    "lastName": "Jackson",
    "phoneNumber": 5698777
  },
  {
    "personId": 4,
    "firstName": "Jessica",
    "lastName": "Simpson",
    "phoneNumber": 369745281
  },
  {
    "personId": 5,
    "firstName": "Tori",
    "lastName": "Kelly",
    "phoneNumber": 879955542
  },
  {
    "personId": 6,
    "firstName": "Sarah",
    "lastName": "Lee",
    "phoneNumber": 99889565
  },
  {
    "personId": 7,
    "firstName": "Peter",
    "lastName": "Moel",
    "phoneNumber": 633789452
  }
]
</pre>

  <h2>GET /persons/{personId}/interests</h2>
  <pre>
    [
  {
    "interestId": 1,
    "title": "Fishing",
    "description": "Fishing in Norway"
  }
]
  </pre>

  <h2>GET /persons/{personId}/links</h2>
  <pre>
    [
  {
    "interestId": 1,
    "title": "Fishing",
    "description": "Fishing in Norway"
  }
]
  </pre>

  <h2>POST/person/{personId}/interests/{interestId}</h2>
  <pre>
    {
  "personInterestId": 1008,
  "fkPersonId": 2,
  "persons": {
    "personId": 2,
    "firstName": "Olle",
    "lastName": "Ottosson",
    "phoneNumber": 56988744
  },
  "fkInterestId": 5,
  "interests": {
    "interestId": 5,
    "title": "Gym",
    "description": "Training at gym"
  }
  </pre>

  <h2>POST/persons/{personId}/interests/{interestId}/links</h2>
<pre>
  {
  "linkId": 1002,
  "url": "https://www.codecademy.com/",
  "fkInterestId": 4,
  "interests": {
    "interestId": 4,
    "title": "Computers",
    "description": "Coding"
  }
}
</pre>
