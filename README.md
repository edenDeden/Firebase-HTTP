# Firebase-HTTP
A C# class for working with Firebase using HTTP.

**Constructor**
```C#
public FireBaseDatabase(string dataBaseUrl) // the base url of the firebase project note: without the '/' at the end
```

**GET**
```C#
FireBaseDatabase fdb = new FireBaseDatabase("https://fir-http-nuget-example.firebaseio.com");
User eden = new User() {Name = "eden",Pass = "12345"}
fdb.GetData("TestPath.json");
// output:
// {"Password":12345,"Username":"eden"}
```
**PUSH**

```C#
fdb.PushData("TestPath/Messages.json",eden);
// output: in Firebase
// TestPath:
//   Messages:
//     [random id]:
//       Name:"eden",
//       Pass:"12345"                    
```
**PUT**
```C#
fdb.SendData("TestPath/Messages.json",eden);
//output: in Firebase
// TestPath:
//   Messages:
//     Name:"eden",
//     Pass:"12345"   
```
**Delete**
```C#
fdb.DeleteData("TestPath/Users.json");
//before: in Firebase
// TestPath:
//   Users:
//     Name:"eden",
//     Pass:"12345"
//   Admins:
//     Name:"noteden",
//     Pass:"34567" 
// -----------------

// TestPath:
//   Admins:
//     Name:"noteden",
//     Pass:"34567" 
```
