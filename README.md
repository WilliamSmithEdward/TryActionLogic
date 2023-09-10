# TryActionLogic
Prototype created from a mentoring session focusing on error handling, generic collections, delegates and lambda expressions. Demonstrates how logic can be passed into a delegate parameter, and used to trap error conditions over an iteration in order to "fail gracefully."

## Overview

The code attempts to open a list of files and logs any errors that occur during the process. It subsequently displays the names of successfully opened files and prints out error messages for the failed ones.

### Benefits of Using This Design Pattern

1. **Error Segregation**: 
   - By separating the error-catching mechanism from the main logic using the `TryAction` helper function, the code becomes cleaner and more readable.
   - Errors are not immediately thrown but are collected, allowing the code to continue processing other items, which is especially beneficial when dealing with batch operations like file opening.

2. **Centralized Error Handling**:
   - The `TryAction` function centralizes the error-handling mechanism. This means any changes to error handling, like logging or specific error manipulations, can be done in one place.

3. **Enhanced Readability**:
   - The main logic remains free of extensive `try-catch` blocks, making it easier to understand the core functionality.

4. **Batch Processing with Feedback**:
   - By processing all files in a batch and then providing feedback (either errors or success messages), the user or system administrator gets a consolidated view of what happened, rather than being interrupted by individual errors.

5. **Scalability**:
   - The pattern can easily be extended or adapted for other batch operations beyond file handling.

6. **Flexibility in Error Reporting**:
   - Errors are collected in a list. This provides flexibility in terms of reporting. For example, one might choose to log all errors at once, display them to a user, or even send them as a batched report via email or another medium.

7. **Predictable Control Flow**:
   - Regardless of whether an error occurs or not, the code flow remains predictable. After attempting to process all files, it systematically checks for errors and responds accordingly.

8. **Opportunity for Enhanced Error Handling**:
   - With this pattern, additional features can easily be incorporated, like retry mechanisms for specific errors or more advanced logging strategies.

### Process Flow

1. Initialize a list to capture any errors (`errorList`).
2. Define a list of file paths (`myFileList`).
3. Attempt to open each file and add the opened `FileStream` to `fileList`.
4. Log any errors that occur during file opening to `errorList`.
5. Print the error messages (if any).
6. Display the names of successfully opened files.
7. If there are any errors, indicate that action needs to be taken (e.g., sending an email).

### Key Components

#### Error List Initialization

```csharp
var errorList = new List<Exception>();
```

#### File Paths

```csharp
List<string> myFileList = new List<string>()
{
    @"C:\Users\willi\Documents\test1.txt",
    ...
};
```

#### Attempt(s) to Open Files

```csharp
var fileList = new List<FileStream>();

myFileList.ForEach(x =>
{
    var error = TryAction(() => fileList.Add(File.Open(x, FileMode.Open)));

    if (error != null) errorList.Add(error);
});
```

#### Display Error Messages

```csharp
errorList.ForEach(x =>
{
    Console.WriteLine(x.Message);
});
```

#### Display File Name(s)

```csharp
fileList.ForEach(x =>
{
    Console.WriteLine(x.Name);
});
```

#### Check for Errors

```csharp
if (errorList.Count == 0)
{
    Console.WriteLine("Everything went great!");
}
else
{
    Console.WriteLine("I need to take action!!");
    // Intention to send an email here
}
```

#### Helper Function: TryAction

This function tries to execute an action. If the action throws an exception, it captures the exception and returns it. If no exceptions are thrown, it returns null.

```csharp
public static Exception TryAction(Action action)
{
    Exception exception = null;

    try
    {
        action();
    }
    catch (Exception ex)
    {
        exception = ex;
    }

    return exception;
}
```
