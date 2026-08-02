# 架构说明

## 目标

Avalonia Workbench 不是单一业务应用，而是一个可复用的桌面应用验证基座。首版优先验证桌面端常见能力，并保持依赖简单、可跨平台构建。

## 分层

- `Models`：纯数据模型和可观察任务模型
- `Services`：模拟数据、文件系统和诊断能力
- `ViewModels`：页面状态、命令与异步工作流
- `Views`：AXAML 页面和少量平台交互代码
- `Styles`：工作台通用样式资源

## 设计选择

1. 使用 CommunityToolkit.Mvvm 减少属性通知和命令模板代码。
2. 启用 Avalonia 编译绑定，尽早发现绑定错误。
3. 文件夹选择器放在 View 的代码后置中，文件扫描逻辑保留在 Service/ViewModel。
4. 后台任务通过 CancellationToken 实现取消，不阻塞 UI 线程。
5. 首版使用免费的 DataGrid，避免依赖 Avalonia Pro 控件。

## 后续演进

当首版跨平台构建和运行稳定后，再拆分 Core、Infrastructure 和 Desktop 项目，引入 SQLite、持久化任务队列、插件边界和 Headless UI 测试。
