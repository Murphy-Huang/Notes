# VR项目

## 20240228：
- SimpleInteractable组件需要Rigidbody运行

## 20240229：
- 使用InputSystem代替XRInputSubsystem

## 20240304: 
- 出现Oculus Intergration缺失导致OVRPlugin丢失报错，疑似因为安装Toolkit的新版sample问题
- animation有不同类型，legacy应该为旧版，没有animtor。Loop的设置地方不同。建议使用新版，legacy只在旧项目使用。
- 导入模型需要分别在Rig导出avatar和在materials导出材质和贴图
- 模型动画（Animation Clip）要在Animation中截取

## Action Maps:
- XRI LeftHand: 添加changelocomotionmode
- XRI RightHand: 添加CreateBall

## Teleport Interactor组件：
- XR Controller 中 Input： select action 和 select action value修改