using XLua;

namespace Solution.NetManager
{
    /// <summary>
    /// 实现IPraseProto，反射Lua确定ProtoBuf协议的解析方式
    /// </summary>
    public class LuaMsgPrase : IParseProtobuf
    {
        // 协议格式
        // [<包头1:1><包头2:1><包头3:1><包头4:1><协议版本号:1><服务端版本号:4><数据长度:4><协议号:4><数据体:n>]
        private LuaTable parse_table;

        [CSharpCallLua]
        public delegate int deLuaDataUnpack(LuaTable table, byte[] buf, int length); // 必须是public，否则CSharpCallLua报错
        private deLuaDataUnpack luaDataUnpack;
        [CSharpCallLua]
        public delegate int deLuaSetNetId(LuaTable table, int net_id); // 必须是public，否则CSharpCallLua报错
        private deLuaSetNetId luaSetNetId;

        public LuaMsgParse(LuaTable tparse_table)
        {
            this.parse_table = tparse_table;
            luaDataUnpack = this.parse_table.Get<deLuaDataUnpack>("DataUnpack");
            luaSetNetId = this.parse_table.Get<deLuaSetNetId>("SetNetId");
        }

        /// <summary>
        /// 设置通讯id标识
        /// </summary>
        /// <param name="net_id"></param>
        public void SetNetId(int net_id)
        {
            if (luaSetNetId != null)
            {
                luaSetNetId.Invoke(parse_table, net_id);
            }
        }


        /// <summary>
        /// 数据压包
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public byte[] DataPack(object msg)
        {
            return null;
        }

        /// <summary>
        /// 数据解包
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public int DataUnpack(byte[] buf, int length)
        {
            int type = 0;
            if (luaDataUnpack != null)
            {
                type = luaDataUnpack(this.parse_table, buf, length);
            }
            else
            {
                Debug.LogError("DataUnpack 监听回调不存在！");
            }
            return type;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            luaDataUnpack = null;
            luaSetNetId = null;
            if (parse_table != null)
            {
                parse_table.Dispose();
                parse_table = null;
            }
        }   
    }
}