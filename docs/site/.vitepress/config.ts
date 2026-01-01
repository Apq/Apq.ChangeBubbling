import { defineConfig } from 'vitepress'

// 中文侧边栏配置
const zhSidebar = {
  '/guide/': [
    {
      text: '入门',
      items: [
        { text: '简介', link: '/guide/' },
        { text: '快速开始', link: '/guide/quick-start' },
        { text: '安装', link: '/guide/installation' }
      ]
    },
    {
      text: '基础',
      items: [
        { text: '节点类型', link: '/guide/node-types' },
        { text: '事件冒泡', link: '/guide/event-bubbling' },
        { text: '消息系统', link: '/guide/messaging' }
      ]
    },
    {
      text: '进阶',
      items: [
        { text: 'Rx 响应式流', link: '/guide/reactive-streams' },
        { text: '事件过滤', link: '/guide/event-filtering' },
        { text: '批量操作', link: '/guide/batch-operations' },
        { text: '背压管线', link: '/guide/dataflow' },
        { text: '快照服务', link: '/guide/snapshot' }
      ]
    },
    {
      text: '深入',
      items: [
        { text: '架构设计', link: '/guide/architecture' },
        { text: '性能优化', link: '/guide/performance' },
        { text: '最佳实践', link: '/guide/best-practices' }
      ]
    }
  ],
  '/api/': [
    {
      text: 'API 参考',
      items: [
        { text: '概述', link: '/api/' },
        { text: '节点类型', link: '/api/nodes' },
        { text: '消息系统', link: '/api/messaging' },
        { text: '事件过滤', link: '/api/filtering' },
        { text: '快照服务', link: '/api/snapshot' },
        { text: 'DocFX API 文档 ↗', link: '/api-reference/api/index.html', target: '_blank' }
      ]
    }
  ],
  '/examples/': [
    {
      text: '示例',
      items: [
        { text: '概述', link: '/examples/' },
        { text: '基础示例', link: '/examples/basic' },
        { text: 'Rx 响应式', link: '/examples/reactive' },
        { text: '事件过滤', link: '/examples/filtering' },
        { text: '快照导出', link: '/examples/snapshot' }
      ]
    }
  ]
}

// 英文侧边栏配置
const enSidebar = {
  '/en/guide/': [
    {
      text: 'Getting Started',
      items: [
        { text: 'Introduction', link: '/en/guide/' },
        { text: 'Quick Start', link: '/en/guide/quick-start' },
        { text: 'Installation', link: '/en/guide/installation' }
      ]
    },
    {
      text: 'Basics',
      items: [
        { text: 'Node Types', link: '/en/guide/node-types' },
        { text: 'Event Bubbling', link: '/en/guide/event-bubbling' },
        { text: 'Messaging', link: '/en/guide/messaging' }
      ]
    },
    {
      text: 'Advanced',
      items: [
        { text: 'Reactive Streams', link: '/en/guide/reactive-streams' },
        { text: 'Event Filtering', link: '/en/guide/event-filtering' },
        { text: 'Batch Operations', link: '/en/guide/batch-operations' },
        { text: 'Dataflow Pipeline', link: '/en/guide/dataflow' },
        { text: 'Snapshot Service', link: '/en/guide/snapshot' }
      ]
    },
    {
      text: 'Deep Dive',
      items: [
        { text: 'Architecture', link: '/en/guide/architecture' },
        { text: 'Performance', link: '/en/guide/performance' },
        { text: 'Best Practices', link: '/en/guide/best-practices' }
      ]
    }
  ],
  '/en/api/': [
    {
      text: 'API Reference',
      items: [
        { text: 'Overview', link: '/en/api/' },
        { text: 'Node Types', link: '/en/api/nodes' },
        { text: 'Messaging', link: '/en/api/messaging' },
        { text: 'Filtering', link: '/en/api/filtering' },
        { text: 'Snapshot', link: '/en/api/snapshot' },
        { text: 'DocFX API Docs ↗', link: '/api-reference/api/index.html', target: '_blank' }
      ]
    }
  ],
  '/en/examples/': [
    {
      text: 'Examples',
      items: [
        { text: 'Overview', link: '/en/examples/' },
        { text: 'Basic Examples', link: '/en/examples/basic' },
        { text: 'Reactive', link: '/en/examples/reactive' },
        { text: 'Filtering', link: '/en/examples/filtering' },
        { text: 'Snapshot', link: '/en/examples/snapshot' }
      ]
    }
  ]
}

// Gitee 图标 SVG
const giteeSvg = '<svg viewBox="0 0 1024 1024" xmlns="http://www.w3.org/2000/svg"><path d="M512 1024C229.222 1024 0 794.778 0 512S229.222 0 512 0s512 229.222 512 512-229.222 512-512 512z m259.149-568.883h-290.74a25.293 25.293 0 0 0-25.292 25.293l-0.026 63.206c0 13.952 11.315 25.293 25.267 25.293h177.024c13.978 0 25.293 11.315 25.293 25.267v12.646a75.853 75.853 0 0 1-75.853 75.853h-240.23a25.293 25.293 0 0 1-25.267-25.293V417.203a75.853 75.853 0 0 1 75.827-75.853h353.946a25.293 25.293 0 0 0 25.267-25.292l0.077-63.207a25.293 25.293 0 0 0-25.268-25.293H417.152a189.62 189.62 0 0 0-189.62 189.645V771.15c0 13.977 11.316 25.293 25.294 25.293h372.94a170.65 170.65 0 0 0 170.65-170.65V480.384a25.293 25.293 0 0 0-25.293-25.267z" fill="currentColor"/></svg>'

export default defineConfig({
  title: 'Apq.ChangeBubbling',

  head: [
    ['link', { rel: 'icon', href: '/logo.svg' }],
    // SEO meta 标签
    ['meta', { name: 'keywords', content: 'Apq.ChangeBubbling, .NET, 变更冒泡, Change Bubbling, Reactive, Rx, MVVM, C#, 事件, Observable' }],
    ['meta', { name: 'author', content: 'Apq' }],
    // Open Graph 标签（社交媒体分享）
    ['meta', { property: 'og:type', content: 'website' }],
    ['meta', { property: 'og:title', content: 'Apq.ChangeBubbling - .NET Change Bubbling Library' }],
    ['meta', { property: 'og:description', content: 'A .NET library for tree-structured change event bubbling with Rx streams, weak messaging, and pluggable scheduling' }],
    ['meta', { property: 'og:image', content: '/logo.svg' }],
    // Twitter Card 标签
    ['meta', { name: 'twitter:card', content: 'summary' }],
    ['meta', { name: 'twitter:title', content: 'Apq.ChangeBubbling - .NET Change Bubbling Library' }],
    ['meta', { name: 'twitter:description', content: 'A .NET library for tree-structured change event bubbling with Rx streams, weak messaging, and pluggable scheduling' }],
    // Sitemap 链接
    ['link', { rel: 'sitemap', type: 'application/xml', href: '/sitemap.xml' }]
  ],

  // 多语言配置
  locales: {
    root: {
      label: '简体中文',
      lang: 'zh-CN',
      description: '变更冒泡事件库，支持 Rx 响应式流、弱引用消息和可插拔调度环境',
      themeConfig: {
        nav: [
          { text: '指南', link: '/guide/', activeMatch: '/guide/' },
          { text: 'API', link: '/api/', activeMatch: '/api/' },
          { text: '示例', link: '/examples/', activeMatch: '/examples/' },
          { text: '更新日志', link: '/changelog' }
        ],
        sidebar: zhSidebar,
        outline: {
          label: '页面导航',
          level: [2, 3]
        },
        darkModeSwitchLabel: '外观',
        darkModeSwitchTitle: '切换到深色模式',
        lightModeSwitchTitle: '切换到浅色模式',
        docFooter: {
          prev: '上一页',
          next: '下一页'
        },
        editLink: {
          pattern: 'https://gitee.com/apq/Apq.ChangeBubbling/edit/master/docs/site/:path',
          text: '在 Gitee 上编辑此页'
        },
        footer: {
          message: '基于 MIT 许可发布',
          copyright: `Copyright © ${new Date().getFullYear()} Apq.ChangeBubbling`
        },
        search: {
          provider: 'local',
          options: {
            translations: {
              button: {
                buttonText: '搜索文档',
                buttonAriaLabel: '搜索文档'
              },
              modal: {
                noResultsText: '无法找到相关结果',
                resetButtonTitle: '清除查询条件',
                footer: {
                  selectText: '选择',
                  navigateText: '切换'
                }
              }
            }
          }
        }
      }
    },
    en: {
      label: 'English',
      lang: 'en-US',
      link: '/en/',
      description: 'Change bubbling event library with Rx streams, weak messaging, and pluggable scheduling',
      themeConfig: {
        nav: [
          { text: 'Guide', link: '/en/guide/', activeMatch: '/en/guide/' },
          { text: 'API', link: '/en/api/', activeMatch: '/en/api/' },
          { text: 'Examples', link: '/en/examples/', activeMatch: '/en/examples/' },
          { text: 'Changelog', link: '/en/changelog' }
        ],
        sidebar: enSidebar,
        outline: {
          label: 'On this page',
          level: [2, 3]
        },
        docFooter: {
          prev: 'Previous',
          next: 'Next'
        },
        editLink: {
          pattern: 'https://gitee.com/apq/Apq.ChangeBubbling/edit/master/docs/site/:path',
          text: 'Edit this page on Gitee'
        },
        footer: {
          message: 'Released under the MIT License',
          copyright: `Copyright © ${new Date().getFullYear()} Apq.ChangeBubbling`
        }
      }
    }
  },

  themeConfig: {
    logo: '/logo.svg',

    socialLinks: [
      {
        icon: { svg: giteeSvg },
        link: 'https://gitee.com/apq/Apq.ChangeBubbling'
      }
    ]
  },

  markdown: {
    lineNumbers: true
  },

  lastUpdated: false,

  // 忽略 localhost 链接
  ignoreDeadLinks: [
    /^http:\/\/localhost/
  ],

  // Sitemap 配置（SEO 优化）
  sitemap: {
    hostname: 'https://apq-changebubbling.gitee.io'
  },

  // Vite 配置：处理 DocFX 静态文件
  vite: {
    publicDir: 'public',
    server: {
      fs: {
        // 允许访问项目目录外的文件
        strict: false
      }
    },
    plugins: [
      {
        name: 'api-reference-redirect',
        configureServer(server) {
          server.middlewares.use((req, res, next) => {
            // 处理 /api-reference/ 路径，重定向到 index.html
            if (req.url === '/api-reference/' || req.url === '/api-reference') {
              res.writeHead(302, { Location: '/api-reference/index.html' })
              res.end()
              return
            }
            next()
          })
        },
        configurePreviewServer(server) {
          server.middlewares.use((req, res, next) => {
            // 预览服务器也需要同样的处理
            if (req.url === '/api-reference/' || req.url === '/api-reference') {
              res.writeHead(302, { Location: '/api-reference/index.html' })
              res.end()
              return
            }
            next()
          })
        }
      }
    ]
  }
})
