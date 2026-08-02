# Avalonia Workbench

一个使用 **Avalonia 12.1 + .NET 10** 构建的跨平台桌面技术工作台，用来验证真实桌面应用中的导航、数据表格、文件系统、异步任务、主题切换、诊断与跨平台发布能力。

## 功能

- **仪表盘**：运行概况、能力覆盖和活动记录
- **数据实验室**：2,500～10,000 条模拟数据、搜索、状态筛选、排序与行虚拟化
- **文件工作区**：原生文件夹选择器、异步目录扫描、文件大小和修改时间展示
- **任务中心**：并发异步任务、进度反馈、取消和清理
- **UI 实验室**：浅色、深色、跟随系统主题，以及常用控件状态展示
- **诊断中心**：操作系统、架构、.NET 运行时、工作集和本地目录信息
- **设置**：语言、状态栏、动画和恢复页面等配置结构

## 技术栈

- .NET 10
- Avalonia 12.1
- CommunityToolkit.Mvvm 8.4
- Avalonia DataGrid
- xUnit
- GitHub Actions

## 本地运行

```bash
dotnet restore AvaloniaWorkbench.slnx
dotnet run --project src/AvaloniaWorkbench/AvaloniaWorkbench.csproj
```

## 测试

```bash
dotnet test AvaloniaWorkbench.slnx
```

## 发布

```bash
dotnet publish src/AvaloniaWorkbench/AvaloniaWorkbench.csproj \
  -c Release \
  -r win-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true
```

仓库中的 GitHub Actions 会自动构建：

- Windows x64
- Linux x64
- macOS x64
- macOS ARM64

首次推送到 `main` 后会创建 `v0.1.0` Release。

## 目录结构

```text
src/AvaloniaWorkbench/
├─ Models/
├─ Services/
├─ Styles/
├─ ViewModels/
└─ Views/

tests/AvaloniaWorkbench.Tests/
```

## 路线图

- SQLite 设置和历史记录持久化
- 文件拖放、缩略图和哈希计算
- CSV 导入导出与列配置
- 可暂停、可恢复的持久化任务队列
- Headless UI 测试和视觉回归测试
- Windows MSIX/安装器与 macOS `.app` 打包

## License

MIT
