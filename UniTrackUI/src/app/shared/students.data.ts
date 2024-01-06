import { StudentProfile } from "./models/student-profile";

export const students = [
  // {
  //   "id": "1",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar1",
  //   "firstName": "John",
  //   "lastName": "Doe",
  //   "classId": "101",
  //   "className": "Mathematics",
  //   "number": 10,
  //   "marks": [
  //     {
  //       "value": 5,
  //       "studentId": "1",
  //       "teacherId": "2",
  //       "subjectId": "1",
  //       "topic": "string",
  //       "gradedOn": "10.10.2023",
  //       "subjectName": "Test",
  //       "teacherFirstName": "Teacher",
  //       "teacherLastName": "Name"
  //     }
  //   ],
  //   "absences": [
  //     {"subject": "English", "absence": 2, "excused": false},
  //     {"subject": "History", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "2",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar2",
  //   "firstName": "Alice",
  //   "lastName": "Johnson",
  //   "classId": "102",
  //   "className": "Science",
  //   "number": 15,
  //   "marks": [
  //     {"value": "5", "subject": "Physics"},
  //     {"value": "6", "subject": "Chemistry"}
  //   ],
  //   "absences": [
  //     {"subject": "Math", "absence": 3, "excused": false},
  //     {"subject": "English", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "3",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar3",
  //   "firstName": "Bob",
  //   "lastName": "Smith",
  //   "classId": "103",
  //   "className": "History",
  //   "number": 20,
  //   "marks": [
  //     {"value": "4", "subjectName": "History"},
  //     {"value": "6", "subjectName": "Geography"}
  //   ],
  //   "absences": [
  //     {"subject": "Science", "absence": 1, "excused": false},
  //     {"subject": "Chemistry", "absence": 2, "excused": true}
  //   ]
  // },
  // {
  //   "id": "4",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar4",
  //   "firstName": "Eva",
  //   "lastName": "Williams",
  //   "classId": "104",
  //   "className": "English",
  //   "number": 25,
  //   "marks": [
  //     {"value": "6", "subject": "English"},
  //     {"value": "5", "subject": "Literature"}
  //   ],
  //   "absences": [
  //     {"subject": "Geography", "absence": 0, "excused": true},
  //     {"subject": "Physics", "absence": 4, "excused": false}
  //   ]
  // },
  // {
  //   "id": "5",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar5",
  //   "firstName": "Charlie",
  //   "lastName": "Brown",
  //   "classId": "105",
  //   "className": "Physics",
  //   "number": 30,
  //   "marks": [
  //     {"value": "5", "subject": "Physics"},
  //     {"value": "6", "subject": "Math"}
  //   ],
  //   "absences": [
  //     {"subject": "Literature", "absence": 1, "excused": false},
  //     {"subject": "History", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "6",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar6",
  //   "firstName": "Diana",
  //   "lastName": "Miller",
  //   "classId": "106",
  //   "className": "Chemistry",
  //   "number": 12,
  //   "marks": [
  //     {"value": "6", "subject": "Chemistry"},
  //     {"value": "5", "subject": "Biology"}
  //   ],
  //   "absences": [
  //     {"subject": "Math", "absence": 2, "excused": false},
  //     {"subject": "English", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "7",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar7",
  //   "firstName": "Frank",
  //   "lastName": "Anderson",
  //   "classId": "107",
  //   "className": "Biology",
  //   "number": 18,
  //   "marks": [
  //     {"value": "5", "subject": "Biology"},
  //     {"value": "4", "subject": "Chemistry"}
  //   ],
  //   "absences": [
  //     {"subject": "Science", "absence": 3, "excused": false},
  //     {"subject": "Literature", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "8",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar8",
  //   "firstName": "Grace",
  //   "lastName": "Taylor",
  //   "classId": "108",
  //   "className": "Literature",
  //   "number": 22,
  //   "marks": [
  //     {"value": "6", "subject": "Literature"},
  //     {"value": "5", "subject": "English"}
  //   ],
  //   "absences": [
  //     {"subject": "History", "absence": 2, "excused": false},
  //     {"subject": "Geography", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "9",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar9",
  //   "firstName": "Harry",
  //   "lastName": "Thomas",
  //   "classId": "109",
  //   "className": "Geography",
  //   "number": 28,
  //   "marks": [
  //     {"value": "5", "subject": "Geography"},
  //     {"value": "6", "subject": "History"}
  //   ],
  //   "absences": [
  //     {"subject": "Math", "absence": 1, "excused": false},
  //     {"subject": "Biology", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "10",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar10",
  //   "firstName": "Ivy",
  //   "lastName": "White",
  //   "classId": "110",
  //   "className": "Mathematics",
  //   "number": 14,
  //   "marks": [
  //     {"value": "6", "subject": "Math"},
  //     {"value": "5", "subject": "Physics"}
  //   ],
  //   "absences": [
  //     {"subject": "Chemistry", "absence": 4, "excused": false},
  //     {"subject": "English", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "11",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar11",
  //   "firstName": "Jack",
  //   "lastName": "Jones",
  //   "classId": "111",
  //   "className": "Science",
  //   "number": 16,
  //   "marks": [
  //     {"value": "4", "subject": "Science"},
  //     {"value": "6", "subject": "Chemistry"}
  //   ],
  //   "absences": [
  //     {"subject": "English", "absence": 2, "excused": false},
  //     {"subject": "History", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "12",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar12",
  //   "firstName": "Karen",
  //   "lastName": "Brown",
  //   "classId": "112",
  //   "className": "History",
  //   "number": 21,
  //   "marks": [
  //     {"value": "6", "subject": "History"},
  //     {"value": "5", "subject": "Geography"}
  //   ],
  //   "absences": [
  //     {"subject": "Science", "absence": 1, "excused": false},
  //     {"subject": "Chemistry", "absence": 3, "excused": true}
  //   ]
  // },
  // {
  //   "id": "13",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar13",
  //   "firstName": "Leo",
  //   "lastName": "Davis",
  //   "classId": "113",
  //   "className": "English",
  //   "number": 26,
  //   "marks": [
  //     {"value": "6", "subject": "English"},
  //     {"value": "5", "subject": "Literature"}
  //   ],
  //   "absences": [
  //     {"subject": "Geography", "absence": 0, "excused": true},
  //     {"subject": "Physics", "absence": 4, "excused": false}
  //   ]
  // },
  // {
  //   "id": "14",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar14",
  //   "firstName": "Mia",
  //   "lastName": "Moore",
  //   "classId": "114",
  //   "className": "Physics",
  //   "number": 31,
  //   "marks": [
  //     {"value": "5", "subject": "Physics"},
  //     {"value": "6", "subject": "Math"}
  //   ],
  //   "absences": [
  //     {"subject": "Literature", "absence": 2, "excused": false},
  //     {"subject": "History", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "15",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar15",
  //   "firstName": "Noah",
  //   "lastName": "Young",
  //   "classId": "115",
  //   "className": "Chemistry",
  //   "number": 13,
  //   "marks": [
  //     {"value": "6", "subject": "Chemistry"},
  //     {"value": "5", "subject": "Biology"}
  //   ],
  //   "absences": [
  //     {"subject": "Math", "absence": 2, "excused": false},
  //     {"subject": "English", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "16",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar16",
  //   "firstName": "Olivia",
  //   "lastName": "Hall",
  //   "classId": "116",
  //   "className": "Biology",
  //   "number": 19,
  //   "marks": [
  //     {"value": "5", "subject": "Biology"},
  //     {"value": "4", "subject": "Chemistry"}
  //   ],
  //   "absences": [
  //     {"subject": "Science", "absence": 3, "excused": false},
  //     {"subject": "Literature", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "17",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar17",
  //   "firstName": "Peter",
  //   "lastName": "Martin",
  //   "classId": "117",
  //   "className": "Literature",
  //   "number": 23,
  //   "marks": [
  //     {"value": "6", "subject": "Literature"},
  //     {"value": "5", "subject": "English"}
  //   ],
  //   "absences": [
  //     {"subject": "History", "absence": 2, "excused": false},
  //     {"subject": "Geography", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "18",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar18",
  //   "firstName": "Quinn",
  //   "lastName": "Miller",
  //   "classId": "118",
  //   "className": "Geography",
  //   "number": 29,
  //   "marks": [
  //     {"value": "5", "subject": "Geography"},
  //     {"value": "6", "subject": "History"}
  //   ],
  //   "absences": [
  //     {"subject": "Math", "absence": 1, "excused": false},
  //     {"subject": "Biology", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "19",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar19",
  //   "firstName": "Ryan",
  //   "lastName": "King",
  //   "classId": "119",
  //   "className": "Mathematics",
  //   "number": 15,
  //   "marks": [
  //     {"value": "6", "subject": "Math"},
  //     {"value": "5", "subject": "Physics"}
  //   ],
  //   "absences": [
  //     {"subject": "Chemistry", "absence": 4, "excused": false},
  //     {"subject": "English", "absence": 1, "excused": true}
  //   ]
  // },
  // {
  //   "id": "20",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar20",
  //   "firstName": "Sara",
  //   "lastName": "Turner",
  //   "classId": "120",
  //   "className": "Science",
  //   "number": 17,
  //   "marks": [
  //     {"value": "4", "subject": "Science"},
  //     {"value": "6", "subject": "Chemistry"}
  //   ],
  //   "absences": [
  //     {"subject": "English", "absence": 2, "excused": false},
  //     {"subject": "History", "absence": 0, "excused": true}
  //   ]
  // },
  // {
  //   "id": "21",
  //   "schoolId": "1",
  //   "type": "STUDENT",
  //   "avatarUrl": "https://example.com/avatar21",
  //   "firstName": "Tom",
  //   "lastName": "Walker",
  //   "classId": "121",
  //   "className": "History",
  //   "number": 20,
  //   "marks": [
  //     {"value": "6", "subject": "History"},
  //     {"value": "5", "subject": "Geography"}
  //   ],
  //   "absences": [
  //     {"subject": "Science", "absence": 1, "excused": false},
  //     {"subject": "Chemistry", "absence": 3, "excused": true}
  //   ]
  // }
] as StudentProfile[];
