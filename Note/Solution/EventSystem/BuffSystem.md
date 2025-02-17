# 仿炉石

可以受buff影响的角色创建自己BuffManager，创建角色的时候将事件注册到BuffManager中（Subscribe）；
角色Buff可以在EventManager的事件触发的时候遍历发送消息（OnNext），也可以被其他角色直接调用（OnNext）；
为每个Buff函数创建好传递的数据类型；

这个方法感觉不太靈活，添加新事件的时候要同时改动BuffManager、EventManager
