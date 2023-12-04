import { StudentProfile } from "./models/student-profile";

export const students = [
  {
    "id": "1",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar1",
    "firstName": "John",
    "lastName": "Doe",
    "classId": "101",
    "className": "Mathematics",
    "number": 10,
    "grades": [
      {"grade": "6", "subject": "Math"},
      {"grade": "5", "subject": "Science"}
    ],
    "absences": [
      {"subject": "English", "absence": 2, "excused": false},
      {"subject": "History", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "2",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar2",
    "firstName": "Alice",
    "lastName": "Johnson",
    "classId": "102",
    "className": "Science",
    "number": 15,
    "grades": [
      {"grade": "5", "subject": "Physics"},
      {"grade": "6", "subject": "Chemistry"}
    ],
    "absences": [
      {"subject": "Math", "absence": 3, "excused": false},
      {"subject": "English", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "3",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar3",
    "firstName": "Bob",
    "lastName": "Smith",
    "classId": "103",
    "className": "History",
    "number": 20,
    "grades": [
      {"grade": "4", "subject": "History"},
      {"grade": "6", "subject": "Geography"}
    ],
    "absences": [
      {"subject": "Science", "absence": 1, "excused": false},
      {"subject": "Chemistry", "absence": 2, "excused": true}
    ]
  },
  {
    "id": "4",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar4",
    "firstName": "Eva",
    "lastName": "Williams",
    "classId": "104",
    "className": "English",
    "number": 25,
    "grades": [
      {"grade": "6", "subject": "English"},
      {"grade": "5", "subject": "Literature"}
    ],
    "absences": [
      {"subject": "Geography", "absence": 0, "excused": true},
      {"subject": "Physics", "absence": 4, "excused": false}
    ]
  },
  {
    "id": "5",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar5",
    "firstName": "Charlie",
    "lastName": "Brown",
    "classId": "105",
    "className": "Physics",
    "number": 30,
    "grades": [
      {"grade": "5", "subject": "Physics"},
      {"grade": "6", "subject": "Math"}
    ],
    "absences": [
      {"subject": "Literature", "absence": 1, "excused": false},
      {"subject": "History", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "6",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar6",
    "firstName": "Diana",
    "lastName": "Miller",
    "classId": "106",
    "className": "Chemistry",
    "number": 12,
    "grades": [
      {"grade": "6", "subject": "Chemistry"},
      {"grade": "5", "subject": "Biology"}
    ],
    "absences": [
      {"subject": "Math", "absence": 2, "excused": false},
      {"subject": "English", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "7",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar7",
    "firstName": "Frank",
    "lastName": "Anderson",
    "classId": "107",
    "className": "Biology",
    "number": 18,
    "grades": [
      {"grade": "5", "subject": "Biology"},
      {"grade": "4", "subject": "Chemistry"}
    ],
    "absences": [
      {"subject": "Science", "absence": 3, "excused": false},
      {"subject": "Literature", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "8",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar8",
    "firstName": "Grace",
    "lastName": "Taylor",
    "classId": "108",
    "className": "Literature",
    "number": 22,
    "grades": [
      {"grade": "6", "subject": "Literature"},
      {"grade": "5", "subject": "English"}
    ],
    "absences": [
      {"subject": "History", "absence": 2, "excused": false},
      {"subject": "Geography", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "9",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar9",
    "firstName": "Harry",
    "lastName": "Thomas",
    "classId": "109",
    "className": "Geography",
    "number": 28,
    "grades": [
      {"grade": "5", "subject": "Geography"},
      {"grade": "6", "subject": "History"}
    ],
    "absences": [
      {"subject": "Math", "absence": 1, "excused": false},
      {"subject": "Biology", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "10",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar10",
    "firstName": "Ivy",
    "lastName": "White",
    "classId": "110",
    "className": "Mathematics",
    "number": 14,
    "grades": [
      {"grade": "6", "subject": "Math"},
      {"grade": "5", "subject": "Physics"}
    ],
    "absences": [
      {"subject": "Chemistry", "absence": 4, "excused": false},
      {"subject": "English", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "11",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar11",
    "firstName": "Jack",
    "lastName": "Jones",
    "classId": "111",
    "className": "Science",
    "number": 16,
    "grades": [
      {"grade": "4", "subject": "Science"},
      {"grade": "6", "subject": "Chemistry"}
    ],
    "absences": [
      {"subject": "English", "absence": 2, "excused": false},
      {"subject": "History", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "12",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar12",
    "firstName": "Karen",
    "lastName": "Brown",
    "classId": "112",
    "className": "History",
    "number": 21,
    "grades": [
      {"grade": "6", "subject": "History"},
      {"grade": "5", "subject": "Geography"}
    ],
    "absences": [
      {"subject": "Science", "absence": 1, "excused": false},
      {"subject": "Chemistry", "absence": 3, "excused": true}
    ]
  },
  {
    "id": "13",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar13",
    "firstName": "Leo",
    "lastName": "Davis",
    "classId": "113",
    "className": "English",
    "number": 26,
    "grades": [
      {"grade": "6", "subject": "English"},
      {"grade": "5", "subject": "Literature"}
    ],
    "absences": [
      {"subject": "Geography", "absence": 0, "excused": true},
      {"subject": "Physics", "absence": 4, "excused": false}
    ]
  },
  {
    "id": "14",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar14",
    "firstName": "Mia",
    "lastName": "Moore",
    "classId": "114",
    "className": "Physics",
    "number": 31,
    "grades": [
      {"grade": "5", "subject": "Physics"},
      {"grade": "6", "subject": "Math"}
    ],
    "absences": [
      {"subject": "Literature", "absence": 2, "excused": false},
      {"subject": "History", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "15",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar15",
    "firstName": "Noah",
    "lastName": "Young",
    "classId": "115",
    "className": "Chemistry",
    "number": 13,
    "grades": [
      {"grade": "6", "subject": "Chemistry"},
      {"grade": "5", "subject": "Biology"}
    ],
    "absences": [
      {"subject": "Math", "absence": 2, "excused": false},
      {"subject": "English", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "16",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar16",
    "firstName": "Olivia",
    "lastName": "Hall",
    "classId": "116",
    "className": "Biology",
    "number": 19,
    "grades": [
      {"grade": "5", "subject": "Biology"},
      {"grade": "4", "subject": "Chemistry"}
    ],
    "absences": [
      {"subject": "Science", "absence": 3, "excused": false},
      {"subject": "Literature", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "17",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar17",
    "firstName": "Peter",
    "lastName": "Martin",
    "classId": "117",
    "className": "Literature",
    "number": 23,
    "grades": [
      {"grade": "6", "subject": "Literature"},
      {"grade": "5", "subject": "English"}
    ],
    "absences": [
      {"subject": "History", "absence": 2, "excused": false},
      {"subject": "Geography", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "18",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar18",
    "firstName": "Quinn",
    "lastName": "Miller",
    "classId": "118",
    "className": "Geography",
    "number": 29,
    "grades": [
      {"grade": "5", "subject": "Geography"},
      {"grade": "6", "subject": "History"}
    ],
    "absences": [
      {"subject": "Math", "absence": 1, "excused": false},
      {"subject": "Biology", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "19",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar19",
    "firstName": "Ryan",
    "lastName": "King",
    "classId": "119",
    "className": "Mathematics",
    "number": 15,
    "grades": [
      {"grade": "6", "subject": "Math"},
      {"grade": "5", "subject": "Physics"}
    ],
    "absences": [
      {"subject": "Chemistry", "absence": 4, "excused": false},
      {"subject": "English", "absence": 1, "excused": true}
    ]
  },
  {
    "id": "20",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar20",
    "firstName": "Sara",
    "lastName": "Turner",
    "classId": "120",
    "className": "Science",
    "number": 17,
    "grades": [
      {"grade": "4", "subject": "Science"},
      {"grade": "6", "subject": "Chemistry"}
    ],
    "absences": [
      {"subject": "English", "absence": 2, "excused": false},
      {"subject": "History", "absence": 0, "excused": true}
    ]
  },
  {
    "id": "21",
    "uniId": "1",
    "type": "STUDENT",
    "avatarUrl": "https://example.com/avatar21",
    "firstName": "Tom",
    "lastName": "Walker",
    "classId": "121",
    "className": "History",
    "number": 20,
    "grades": [
      {"grade": "6", "subject": "History"},
      {"grade": "5", "subject": "Geography"}
    ],
    "absences": [
      {"subject": "Science", "absence": 1, "excused": false},
      {"subject": "Chemistry", "absence": 3, "excused": true}
    ]
  }
] as StudentProfile[];
