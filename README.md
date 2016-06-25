# EasyThrottling

This project demonstrates how throttling can be implemented. The main  class is [EasyThrotting](https://github.com/NickolaiA/EasyThrottling/blob/master/EasyThrottling/EasyThrottling.cs).

Using the EasyThrottling is really easy - create an instance of EasyThrottling class and use it before calling some method (or just at the very beginning of such method) which must be executed N times every M seconds. The following code configures Throttling object to execute 5 requests every second:

```
Throttle throttle = new Throttle(new TimeSpan(0, 0, 1), 5);
```

The included console application in the source code executes 5 tasks and allows only 3 of them to be executed every 2 seconds. It prints the following:

```
Allowed - 1 of 3 tasks in progress
Allowed - 2 of 3 tasks in progress
Task #4 iteration 1
Allowed - 3 of 3 tasks in progress
Task #2 iteration 1
Busy - Waiting before executing the next task... Number of active tasks 3
Task #1 iteration 1
Continue - 1 of 3 tasks in progress
Task #3 iteration 1
Allowed - 2 of 3 tasks in progress
Task #5 iteration 1
Allowed - 3 of 3 tasks in progress
Task #2 iteration 2
Busy - Waiting before executing the next task... Number of active tasks 3
Continue - 1 of 3 tasks in progress
Task #1 iteration 2
Allowed - 2 of 3 tasks in progress
Task #4 iteration 2
Allowed - 3 of 3 tasks in progress
Task #5 iteration 2
Busy - Waiting before executing the next task... Number of active tasks 3
Continue - 1 of 3 tasks in progress
Task #3 iteration 2
Allowed - 2 of 3 tasks in progress
Task #2 iteration 3
Allowed - 3 of 3 tasks in progress
Task #4 iteration 3
Busy - Waiting before executing the next task... Number of active tasks 3
Continue - 1 of 3 tasks in progress
Task #1 iteration 3
Allowed - 2 of 3 tasks in progress
Task #5 iteration 3
Allowed - 3 of 3 tasks in progress
Task #2 iteration 4
Busy - Waiting before executing the next task... Number of active tasks 3
Continue - 1 of 3 tasks in progress
Task #3 iteration 3
Allowed - 2 of 3 tasks in progress
Task #4 iteration 4
Allowed - 3 of 3 tasks in progress
Task #1 iteration 4
Busy - Waiting before executing the next task... Number of active tasks 3
Continue - 1 of 3 tasks in progress
Task #5 iteration 4
Allowed - 2 of 3 tasks in progress
Task #2 iteration 5
Allowed - 3 of 3 tasks in progress
Task #4 iteration 5
Busy - Waiting before executing the next task... Number of active tasks 3
Continue - 1 of 3 tasks in progress
Task #3 iteration 4
Allowed - 2 of 3 tasks in progress
Task #1 iteration 5
Finish task #2
Allowed - 3 of 3 tasks in progress
Task #5 iteration 5
Finish task #4
Allowed - 1 of 3 tasks in progress
Task #3 iteration 5
Finish task #1
Finish task #5
Finish task #3
```
