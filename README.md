# Firebase-HTTP
A C# class for working with Firebase using HTTP.

**Constructor**
```C#
public FireBaseDatabase(string dataBaseUrl) // the base url of the firebase project note: without the '/' at the end
```

**GET**
```C#
FireBaseDatabase fdb = new FireBaseDatabase("https://fir-http-nuget-example.firebaseio.com");

fdb.GetData("TestPath.json");
// output:
// {"Password":12345,"Username":"eden"}
```
**PUSH**

```C#
fdb.PushData("TestPath/Messages.json",User);
// output: in Firebase
// TextPath:
//   Messages:
//     [random id]:
//       Name:"eden",
//       Pass:"12345"                    
                                
```
