#include <LinearMath/btThreads.h>

#include "btThreads_wrap.h"

btITaskScheduler* btThreads_btGetSequentialTaskScheduler()
{
	return btGetSequentialTaskScheduler();
}

btITaskScheduler* btThreads_btGetOpenMPTaskScheduler()
{
	return btGetOpenMPTaskScheduler();
}

btITaskScheduler* btThreads_btGetPPLTaskScheduler()
{
	return btGetPPLTaskScheduler();
}

btITaskScheduler* btThreads_btGetTBBTaskScheduler()
{
	return btGetTBBTaskScheduler();
}

void btThreads_btSetTaskScheduler(btITaskScheduler* ts)
{
	btSetTaskScheduler(ts);
}
