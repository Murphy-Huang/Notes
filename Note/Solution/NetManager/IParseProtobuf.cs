namespace Solution.NetManager
{
    public interface IParseProtobuf : IParseProtocol
    {
        /// <summary>
        /// 设置通讯id标识
        /// </summary>
        /// <param name="net_id"></param>
        void SetNetId(int net_id);

        /// <summary>
        /// 数据压包方法
        /// </summary>
        /// <param name="msg">消息体</param>
        byte[] DataPack(object msg);

        /// <summary>
        /// 数据解包方法
        /// </summary>
        /// <param name="buf"></param>
        /// <returns>数字：大于0完整的协议，返回为协议总长度；0协议不存在；-1不合法的协议，可能受到攻击；-2数据包不完整</returns>
        int DataUnpack(byte[] buf, int length);


        /// <summary>
        /// 销毁数据
        /// </summary>
        void Destory();
    }
}