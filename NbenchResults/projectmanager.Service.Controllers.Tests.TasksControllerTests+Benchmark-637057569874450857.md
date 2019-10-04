# projectmanager.Service.Controllers.Tests.TasksControllerTests+Benchmark
__Test to ensure that a minimal throughput test can be rapidly executed.__
_10/4/2019 3:36:27 AM_
### System Info
```ini
NBench=NBench, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=4
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
WorkerThreads=32767, IOThreads=4
```

### NBench Settings
```ini
RunMode=Iterations, TestMode=Measurement
NumberOfIterations=13, MaximumRunTime=00:10:00
Concurrent=True
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] TestCounter |      operations |            1.00 |            1.00 |            1.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|[Counter] TestCounter |      operations |    2,000,000.00 |    1,400,660.93 |      434,782.61 |      516,853.31 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |          500.00 |
|               2 |            0.00 |            0.00 |          600.00 |
|               3 |            0.00 |            0.00 |        1,000.00 |
|               4 |            0.00 |            0.00 |          800.00 |
|               5 |            0.00 |            0.00 |          500.00 |
|               6 |            0.00 |            0.00 |        1,800.00 |
|               7 |            0.00 |            0.00 |          500.00 |
|               8 |            0.00 |            0.00 |          600.00 |
|               9 |            0.00 |            0.00 |          900.00 |
|              10 |            0.00 |            0.00 |        2,300.00 |
|              11 |            0.00 |            0.00 |          700.00 |
|              12 |            0.00 |            0.00 |          700.00 |
|              13 |            0.00 |            0.00 |          600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |          500.00 |
|               2 |            0.00 |            0.00 |          600.00 |
|               3 |            0.00 |            0.00 |        1,000.00 |
|               4 |            0.00 |            0.00 |          800.00 |
|               5 |            0.00 |            0.00 |          500.00 |
|               6 |            0.00 |            0.00 |        1,800.00 |
|               7 |            0.00 |            0.00 |          500.00 |
|               8 |            0.00 |            0.00 |          600.00 |
|               9 |            0.00 |            0.00 |          900.00 |
|              10 |            0.00 |            0.00 |        2,300.00 |
|              11 |            0.00 |            0.00 |          700.00 |
|              12 |            0.00 |            0.00 |          700.00 |
|              13 |            0.00 |            0.00 |          600.00 |

#### [Counter] TestCounter
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |    2,000,000.00 |          500.00 |
|               2 |            1.00 |    1,666,666.67 |          600.00 |
|               3 |            1.00 |    1,000,000.00 |        1,000.00 |
|               4 |            1.00 |    1,250,000.00 |          800.00 |
|               5 |            1.00 |    2,000,000.00 |          500.00 |
|               6 |            1.00 |      555,555.56 |        1,800.00 |
|               7 |            1.00 |    2,000,000.00 |          500.00 |
|               8 |            1.00 |    1,666,666.67 |          600.00 |
|               9 |            1.00 |    1,111,111.11 |          900.00 |
|              10 |            1.00 |      434,782.61 |        2,300.00 |
|              11 |            1.00 |    1,428,571.43 |          700.00 |
|              12 |            1.00 |    1,428,571.43 |          700.00 |
|              13 |            1.00 |    1,666,666.67 |          600.00 |


