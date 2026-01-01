# DocFX API 文档生成指南

本文档介绍如何使用 DocFX 生成 Apq.ChangeBubbling 的 API 参考文档。

## 前置条件

- .NET SDK 8.0 或更高版本
- DocFX 工具

## 安装 DocFX

```bash
dotnet tool install -g docfx
```

验证安装：

```bash
docfx --version
```

## 生成文档

在 `docs/docfx` 目录执行：

```bash
cd docs/docfx

# 生成 API 元数据（从源代码提取 XML 注释）
docfx metadata

# 构建 HTML 文档
docfx build
```

或者一步完成：

```bash
cd docs/docfx
docfx
```

## 配置说明

DocFX 配置文件位于 `docs/docfx/docfx.json`：

### metadata 配置

| 配置项 | 值 | 说明 |
|--------|-----|------|
| `src.files` | `Apq.ChangeBubbling.csproj` | 源项目文件 |
| `dest` | `api/` | 元数据输出目录 |
| `properties.TargetFramework` | `net10.0` | 目标框架 |
| `namespaceLayout` | `flattened` | 命名空间扁平化显示 |
| `memberLayout` | `samePage` | 成员在同一页面显示 |

### build 配置

| 配置项 | 值 | 说明 |
|--------|-----|------|
| `output` | `docs/site/public/api-reference` | 文档输出目录 |
| `template` | `default`, `modern` | 使用的模板 |
| `globalMetadata._appTitle` | `Apq.ChangeBubbling API` | 站点标题 |
| `globalMetadata._enableSearch` | `true` | 启用搜索功能 |

## 输出目录结构

```
docs/site/public/api-reference/
├── api/                    # API 文档页面
│   ├── index.html         # API 首页
│   ├── toc.html           # 目录
│   └── Apq.ChangeBubbling.*.html  # 各命名空间/类型页面
├── public/                 # 静态资源
│   ├── docfx.min.js
│   └── main.css
├── index.html             # 文档首页
├── toc.html               # 顶级目录
└── logo.svg               # Logo（来自 docs/site/public/logo.svg）
```

## 目录结构

DocFX 相关文件位于 `docs/docfx/` 目录：

```
docs/docfx/
├── docfx.json             # DocFX 配置文件
├── index.md               # 文档首页内容
├── toc.yml                # 顶级目录配置
└── api/                   # 生成的 API 元数据（自动生成）
```

## 与 VitePress 集成

生成的 DocFX 文档会自动集成到 VitePress 站点：

- 访问路径：`/api-reference/api/index.html`
- 在 VitePress 侧边栏中显示为 "DocFX API 文档 ↗"
- 点击后在新窗口中打开

## 自定义文档

### 添加 API 首页内容

编辑 `docs/docfx/index.md` 文件：

```markdown
# Apq.ChangeBubbling API 参考

欢迎使用 Apq.ChangeBubbling API 文档。

## 快速导航

- [API 参考](api/index.md) - 完整的 API 文档
```

### 添加 Logo

Logo 文件使用 `docs/site/public/logo.svg`，DocFX 构建时会自动复制到输出目录。

## 常见问题

### Q: 文档生成失败，提示编译错误

确保项目可以正常编译：

```bash
dotnet build
```

### Q: XML 注释没有显示

检查项目文件是否启用了 XML 文档生成：

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

### Q: 如何更新文档

每次代码变更后，重新执行 `docfx` 命令即可更新文档。

## 参考链接

- [DocFX 官方文档](https://dotnet.github.io/docfx/)
- [DocFX GitHub](https://github.com/dotnet/docfx)
