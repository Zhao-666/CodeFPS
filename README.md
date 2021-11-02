# Code:FPS
使用Unity3D引擎实现的一款FPS游戏。

游戏主要内容为使用LowPoly风格仿制的《使命召唤4》新手训练场。
## 使用方法
将所有代码放入Unity的Assets目录中即可。
为了方便开发渲染，Lightmap Resolution调得比较低，要提高画质可以稍微调高。

想要试玩的同学，[点击这里下载](https://github.com/Zhao-666/CodeFPS/releases/download/BetaV0.4/CodeFPSBetaV0.4.rar)
## 效果图
**版本：BetaV0.4.1**

场景：游戏开始界面
![例图3](https://github.com/Zhao-666/CodeFPS/blob/master/Doc/DemoImage/BetaV0.4.1.png)

**版本：BetaV0.2**

场景：游戏开始界面
![例图2](https://github.com/Zhao-666/CodeFPS/blob/master/Doc/DemoImage/BetaV0.2.png)

**版本：BetaV0.1**

场景：训练靶场
![例图1](https://github.com/Zhao-666/CodeFPS/blob/master/Doc/DemoImage/BetaV0.1.png)
## 项目背景
《使命召唤6》是我的启蒙单机游戏。

在很多年前第一次玩到《使命召唤6》的时候，我就被这种电影版的体验震撼了。

原来游戏并不是只有网游，原来单机游戏也可以这么好玩？原来游戏还可以做成这样？

之后开始了十年单机游戏生涯，有幸玩到了COD4/5/7/8/9/10、荣誉勋章、显卡危机、GTA、鬼泣、NFS、马里奥系列、塞尔达传说等优秀作品。

也因此才走上了软件开发的道路。

我认为一个好游戏并不是由画质决定的，新手引导、关卡搭建、流程节奏、剧情走向、音乐音效等非常多的因素都会影响一个游戏带来的体验感。

这个项目使用LowPoly风格仿制《使命召唤4》新手训练场，回味一下这个经典的关卡。

希望你玩的开心。
## 项目规划
### 项目信息
软件版本
- Unity：2018.4.36f1 Personal
- UI：UGUI
- DOTween：DOTween_1_2_632
- 开发工具：JetBrain Rider 2020.1.1
- 开发环境：Windows10 企业版 20H2

使用Unity商店资源包
- Low Poly FPS Pack
- Low Poly Dungeons Lite
- Low Poly Weapons VOL.1
- Low Poly Storage Pack
- POLYGON Starter Pack
- Low Poly Soldiers Demo
- Low Poly Western Saloon

### 版本规划
#### Beta V0.4.1（已发布，2021-11-02）
**主体功能**
- 修复手枪状态下切西瓜没有声音的bug
- 调整场景物件

**GUI功能**
- 优化文字颜色，提高亮度
#### Beta V0.4（已发布，2021-11-01）
**主体功能**
- 增加G36C、USP、MP5枪械
- 增加射击靶位数字标识
- 增加开门模型
- 增加切西瓜剧情
- 外景场景布置
- 完成基础训练场教程
- 优化Start界面，修改天空盒
- 优化模型场景
- 优化枪支弹道
- 优化进入游戏时的声音
- 修复切枪时导致弹匣没有复位的bug
- 通过调整灯光、阴影，降低SetPass calls为原来的一半

**GUI功能**
- 完成准星显示
#### Beta V0.3（已发布，2021-10-23）
**主体功能**
- 调整枪支：开镜状态取消晃动效果，优化枪支晃动幅度、优化枪支后坐力
- 增加游戏中按ESC键弹出菜单栏
- 修复枪支晃动情况下切枪导致的偏移

**GUI功能**
- 修复GUI兼容性问题
- 优化按钮悬停、点击效果。组件化UI界面，统一规格
- 增加游戏帧数显示
- 增加选项菜单，可调整帧数、鼠标灵敏度
#### Beta V0.2（已发布，2021-10-21）
**主体功能**
- 游戏进入界面场景
- 游戏背景音乐
- 枪支灵敏度调整

**GUI功能**
- 游戏开始界面菜单选项
- 新游戏Loading界面
- 制作人页面
- 退出游戏功能
#### Beta V0.1（已发布，2021-10-22）
**主体功能**
- 拾取枪支
- 步枪/手枪切换
- 靶场场景搭建
- 地板、墙壁、天花板
- 武器库
- 训练靶场
- 士兵游戏人物、休息台
- 灯光布局、烘培
- 靶子、油桶等射击目标
- 【训练场流程规划】功能开发
- 优化枪支子弹弹道，实现子弹扩散
- 优化枪支射击，实现后坐力
- 增加训练场计时射击功能

**GUI功能**
- 枪支图标切换
- 拾取武器提示
- 流程提示文案显示
- 增加UI下部聊天文字信息

### 一期需求列表（练习场）
#### 2021-10-29新增需求
**主体功能**
- 增加G36C、USP、MP5枪械（已完成，8.5h）

#### 2021-10-26新增需求
**主体功能**
- 增加切西瓜剧情（已完成，1.5h）
- 增加NPC移动功能

**GUI功能**
- 增加手机端兼容

#### 2021-10-24新增需求
**主体功能**
- 增加体力系统

#### 2021-10-21新增需求
**主体功能**
- 计时训练积分保存功能
- 游戏存档功能
- 射击靶位增加数字标识（已完成，0.5h）

**GUI功能**
- 选项配置页面能够自定义鼠标灵敏度（已完成，5.5h）
- 游戏中按ESC键弹出菜单栏（已完成，2h）
- 修复游戏GUI兼容问题（已完成，0.5h）（叶师傅神他吗找了个2240x1400的显示器来测，这是什么鬼分辨率？）
- 优化菜单选项悬停高亮（已完成，1h）
- 增加帧数显示（已完成，1h）

#### 2021-10-20新增需求
**主体功能**
- 资源压缩解压功能
- Bug：枪械晃动情况下切枪有一定概率造成枪械偏移（已修复，0.5h）

#### 2021-10-19新增需求
**主体功能**
- 增加训练场计时射击功能（已完成，3h）
- 增加游戏开始界面场景（已完成，7h）

#### 2021-10-17新增需求
**主体功能**
- Bug：装弹时切枪会立刻装填完毕。(已修复，0.5h）
    - 使用动画Event触发ReloadFinish方法。

#### 2021-10-15新增需求
**主体功能**
- 增加游戏人物语音（已搁置，5h）（机器翻译语音太过生硬。）

**GUI功能**
- 增加UI下部聊天文字信息（已完成，4h）

#### 2021-10-14新增需求（做不完了啊！！！）
**主体功能**
- 优化枪支子弹弹道，实现子弹扩散。（已完成，0.5h）
- 优化枪支射击，实现后坐力。（已完成，1.5h）

#### 2021-10-11需求
**主体功能**
- 拾取枪支（已完成，耗时2h）
- 步枪/手枪切换（已完成，耗时2h）
- 靶场场景搭建
    - 地板、墙壁、天花板（已完成，耗时5.5h）
    - 武器库（已完成，耗时3h）
    - 训练靶场（已完成，耗时1h）
    - 士兵游戏人物、休息台（已完成，耗时5h）
    - 灯光布局、烘培（已完成，耗时22h）
- 靶子、油桶等射击目标（已完成，耗时2.5h）
- 【训练场流程规划】功能开发（已完成，耗时11h）

**GUI功能**
- 枪支图标切换（已完成，耗时1h）
- 拾取武器提示（已完成，耗时1h）
- 准星显示（已完成，耗时2h）
- 流程提示文案显示（已完成，耗时2h）

### 二期需求列表（实战练习场）
**主体功能**
- 爬梯、滑索功能
- 实战场景搭建（进行中，4h）
- 实战训练计时功能

## 更新日志
### 2021-11-02
1. 优化文字颜色，提高亮度（0.5h）
2. 修复手枪状态下切西瓜没有声音的bug（0.5h）
3. 场景优化（1h）
4. 发布Beta v0.4.1版本

### 2021-11-01
1. 优化G36C模型（1h）
    - 调节主摄像机FOV，增加视野距离。
    - 使用Fade材质球实现瞄准器透镜。
2. 完善基础训练场教程（1h）
    - 结束后打开训练场门。
3. 制作USP、MP5模型（4h）
    - 有了前一次的制作经验这次效率快了很多，但是找合适的MP5模型花了不少时间。
4. 修复切枪时导致弹匣没有复位的bug（0.5h）
    - 自制模型在换弹时通过绑定弹匣和手来模拟换弹动画，脚本OnDisplay时需要复位。
5. 优化Start界面（1.5h）
    - 引入新的天空盒，增加了枪支。
6. 优化模型场景（3h）
    - 删除不必要的模型，减少SetPass Calls，提高性能
7. 发布Beta v0.4版本。

### 2021-10-31
1. 增加G36C模型（3h）
2. 优化枪支随机弹道（0.5h）
    - 枪械模型偏右，随机数生成时往左补偿一点。
### 2021-10-29
感冒了状态不太好。
1. 完成准星显示（2h）
    - 使用4个UI Image，射击或人物移动时扩大Image坐标
2. 射击靶位增加数字标识（0.5h）
    - 使用UGUI设置Render Mode为World Space实现 
3. 场景布置（4h）
    - 更换天空盒，布置场景地图

### 2021-10-28
1. 增加西瓜模型（1h）
2. 增加开门模型（0.5h）
3. 这几天都在剪辑视频没有更新游戏，明天开始恢复正常开发状态

### 2021-10-26
1. 增加切西瓜剧情(1.5h)
    - 因为没找到西瓜模型所以就用手雷代替了（又不是不能用）

### 2021-10-25
1. 优化进入游戏时的声音（1h）
    - 靶子初始化时不播放倒下的声音。
2. 经过热心网友的辨认手枪确实是Glock，修改USP为Glock（0.5h）

### 2021-10-24
1. 通过调整灯光、阴影，降低SetPass calls为原来的一半（2h）
    - 调整了部分重叠的灯光，调整Soft Shadows为Hard Shadows。
2. 优化AK47子弹扩散范围（0.5h）
    - 因为0.2版本增加了纵向后坐力，0.3版本增加了横向后坐力，缩小了原来的扩散范围。

### 遇到问题
**问题一**：如何判断性能瓶颈？进行性能优化？

解决方法：目前通过查看Stats窗口判断性能开销，
测试的时候发现其实更多的优化点不在于代码而是场景、灯光、阴影，
目前这方面知识储备不够没法进行系统的优化。

### 2021-10-23
1. 将帧数调整**输入框**修改为**下拉选项**（1.5h)
    - 增加可调帧数后，打包出来的游戏会有撕裂效果，查资料后发现是因为垂直同步关闭造成的。
    - 通过调整VSyncCount参数实现帧数限制。
2. 调整枪支：开镜状态取消晃动效果，优化枪支晃动幅度、优化枪支后坐力（4h）
    - 增加了横向后坐力，手感这个东西真的太难调了。
3. 增加游戏中按ESC键弹出菜单栏（2h）
    - 因为使用了开始界面的选项面板预制体，处理逻辑花了比较多时间
4. 发布Beta v0.3版本。

#### 遇到问题
**问题一**：不开启垂直同步才可以调整帧数，而因为没有了垂直同步画面就会有很强的撕裂感。
其他游戏是怎么实现垂直同步和帧数调整分开的？

### 2021-10-22
1. 修复GUI兼容性问题（0.5h）
2. 修复枪支晃动情况下切枪导致的偏移（0.5h）
    - 原因是枪支有晃动计算，切枪导致模型active=false停止计算，再次激活模型就会改变模型原始坐标。
3. 优化按钮悬停、点击效果。组件化UI界面，统一规格（1h）
4. 增加游戏帧数显示（1h）
5. 增加选项菜单，可调整帧数、鼠标灵敏度（3h）
    - 使用C#的事件机制实现订阅模式，能够很方便的实现菜单项数据保存
    
#### 遇到问题
**问题一**：调用事件的时候Rider提示使用Invoke()写法，和直接调用有什么区别吗？

解决方法：两种写法完全一样，C#在编译器会将event()转换为event.Invoke()。

### 2021-10-21
1. 制作游戏开始界面。（3h）
2. 制作游戏音效。（5h）
    - 找免费的文字转语音工具找了比较久，人肉识别游戏语音也花了很多时间。（一句一句一遍一遍的听游戏语音真TM痛苦）
3. 发布Beta v0.2版本。

### 2021-10-20
1. 优化Lightmap选项。（1h）
2. 灯光渲染调整。（3h，真的太太太花时间了，这方面知识太过薄弱，不知道他们之间的影响关系）
3. 发布Beta v0.1版本。
4. 制作游戏开始界面（4h）。
    - 没有美术人员的情况下，使用CSS实现了渐变色背景条。（真TM亏我学了前端）

#### 遇到问题
**问题一**：生成静态Lightmap时发现灯光之间会有一些相互影响的关系，
某个灯太亮或者范围太大就会影响另一盏灯，这些知识有一些系统性的教学吗？

### 2021-10-19
1. 实现了木板目标穿透功能。（1.5h）
    - 一开始想用Trigger实现，但是子弹速度太快无法检测到。现在的实现方式为使用射线检测。
2. 优化训练流程5，使用木板遮挡目标。（0.5h）
3. 优化枪支功能：修复了换弹时切枪立刻装填完毕的bug。增加子弹随机弹道。增加后坐力（2h）
4. 增加训练场计时射击功能。（3h）
5. 优化天花板和地板，使用单个模型（1h）

#### 遇到问题
**问题一**：生成Lightmap非常耗时，尝试把Realtime Lightmap关闭后渲染时间大幅下降，
使用了混合光源后，是否需要生成Realtime Lightmap会有什么区别？

### 2021-10-18
1. 优化灯饰，取消往上的SpotLight，使用Emission材质球实现发光效果。（0.5h）
2. 静态灯光渲染。（8h，有很多不懂的地方还是做不太好）
3. 场景看台布局。（2h）
4. PostProcess后期渲染配置。（2h）

#### 遇到问题
**问题一**：渲染时经常会有警告UV Overlap，查资料后是生成出来的光照UV图有重叠部分。

解决方法：主要靠三个参数：Scale In Lightmap（增大物体的光照UV图）、Stitch Seams（接缝缝合）、Pack Margin（增大UV图相邻图表之间的边距）。但是究竟怎么去调优还不太会。

#### 今日总结
花费了太多时间在渲染上面，但是渲染真的是一个大坑，灯光、阴影、静态光照、动态光照、后期渲染，太多参数会影响显示效果了。
应该先用白模实现主体功能再去慢慢渲染。

### 2021-10-17
1. 场景天花板、墙壁布局，武器库细节优化。（3h）
2. 增加武器库、墙壁灯饰组件，进行灯光渲染。（3h）
    - 灯光搭配真的很复杂，各种参数、不同光源都会造成很大的变化。
    
### 遇到问题
**问题一**：相邻的两块墙壁颜色相差很大，查看后发现生成的Baked Lightmap使用了不同的两张图。

解决方法：暂无。

### 2021-10-16
1. 制作灯饰组件，布局射击场灯饰、灯光探头组、反射探头组。（5h）
    - 模型中没有做好的灯饰组件，使用SpotLight组件实现向下灯光，使用第二个SpotLight实现向上灯光，做出灯具阴影，使用PointLight照亮灯具附近区域。
    - 靶子为非静态物体，添加灯光探头组、反射探头组使光照更协调。
    
#### 遇到问题
**问题一**：部分物体在烘焙后光照区域一半暗一半亮，但是在实时模式下没有问题。

解决方法：查看物体模型发现并没有采用独立UV，仅仅是一张贴图导致烘焙的灯光很奇怪，开启【Generate Lightmap UV】选项后解决。

**问题二**：灯光模式设置为mixed后，部分物体亮度不正确。

解决方法：将灯光RenderMode设置为important后解决。

#### 记录
不应该花太多时间在现阶段调整灯光烘培，等全部场景搭建完再调整。

### 2021-10-15
#### 完成功能
1. 优化角色切换枪支代码。
2. 完成【训练场流程规划】阶段8。
3. 完成聊天信息UI展示。
    - 使用协程控制聊天文字的展示和隐藏时间。
4. 增加士兵模型人物。
    - 由于免费资源包中没有合适的站姿动画，手动调节人物关节角度拼凑出站姿。（不会动画只能凑合着调了）
5. 修复拾取枪支声音问题。
    - 原模型中拾取枪支不会播放上膛声音，通过赋值AudioSource的time属性来截取出上膛声音。
    
#### 遇到问题
**问题一**： 通过协程控制聊天文字导致代码有点凌乱，有点类似于埋点操作。

解决方法：暂无。

疑惑：商业游戏是怎么实现的？

**问题二**：游戏开发工作量也太大了把，好鸡儿累啊。。。但是不会感到厌倦，纯属累了

解决方法：不可能有。

### 2021-10-14
#### 完成功能
1. 完成【训练场流程规划】阶段0-7。
2. 经过叶师傅提议优化了AK47的子弹生成点，提高子弹精准度。
    - 原始模型的子弹生成点较高，降低到枪口后在开镜状态下会射不准，优化方式为稍微降低一点并增加Y轴角度。
3. 优化训练靶子大小和距离。
4. 实现子弹射击物品产生弹痕效果。
    - 当子弹的碰撞器触发碰撞的时候，判断碰撞体Tag标签并生成弹痕。

#### 遇到问题
**问题一**： 训练场流程分了许多个阶段，而每个阶段里还可能有子阶段，如果全部功能代码都写在一个类里通过if判断，很容易导致文件变得特别巨大，后期难以维护。

解决方法： 将不同阶段逻辑拆解成独立的Script脚本，创建一个Manager来管理流程的运行。

疑惑：商业游戏是怎么实现的？

### 2021-10-13
#### 完成功能
1. UGUI分辨率自适应
    - 通过使用Canvas Scaler组件，将Scale Mode改为Scale with Screen Size完成自适应功能。
2. 靶场场景搭建
3. 修改资源包中的靶子脚本
    - 由自动击倒起立改为手动执行，以满足教学流程。
4. 制作木板靶子
5. 使用UGUI+DOTween实现流程提示文案显示。
    - 需要为对象添加Canvas Group组件，修改组件的Alpha值。
    - 引入DOTween可以很方便的实现淡入淡出效果（DOFade）。
6. 完成【训练场流程规划】阶段0，实现了拾取AK提示。
    
#### 遇到问题
1. 手误操作导致模型组件丢失，原本打算从Git重新Clone一份下来，发现Clone下来的组建也一样会丢失。
查找到问题是gitignore在提交时忽略了meta文件，导致Clone的预制体无法使用。

### 2021-10-12
#### 完成功能
1. 拾取枪支
    - 使用Camera和Physics组件进行射线检测，检测到对象标签为Weapon时弹出拾取UI提示。
        - 注意事项：对象需具有Collider组件才可被射线检测。
2. 步枪/手枪切换。
    - 通过检测按键Alpha1、Alpha2进行步枪、手枪的GameObject激活与隐藏，完成切换效果。
3. 场景地板、围墙搭建，武器库搭建。
    - 花费太多时间在场景搭建中，可以先使用Cube等物体作为临时场景搭建。
