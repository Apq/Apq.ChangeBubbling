<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useRoute } from 'vitepress'
import DefaultTheme from 'vitepress/theme'

const { Layout } = DefaultTheme
const route = useRoute()

const sidebarCollapsed = ref(false)

// 根据当前路径找到最佳匹配的链接
function findBestMatchingLink(sidebar: Element, currentPath: string): { group: Element | null, link: Element | null } {
  const allGroups = sidebar.querySelectorAll('.VPSidebarItem.level-0.collapsible')
  const normalizedCurrentPath = currentPath.replace(/\.html$/, '').replace(/\/$/, '')

  // 首先检查是否有 VitePress 自动标记的 is-active 链接
  const activeLink = sidebar.querySelector('.VPSidebarItem a.is-active')
  if (activeLink) {
    // 找到包含该链接的分组
    for (const group of allGroups) {
      if (group.contains(activeLink)) {
        return { group, link: activeLink }
      }
    }
  }

  // 如果没有 is-active，尝试根据 URL 路径匹配
  // 找到最长匹配的链接（最具体的匹配）
  let bestMatch: { group: Element | null, link: Element | null, matchLength: number } = { group: null, link: null, matchLength: 0 }

  for (const group of allGroups) {
    const links = group.querySelectorAll('a')
    for (const link of links) {
      const href = link.getAttribute('href') || ''
      const linkPath = href.replace(/\.html$/, '').replace(/\/$/, '')

      if (normalizedCurrentPath.startsWith(linkPath) && linkPath.length > bestMatch.matchLength) {
        bestMatch = { group, link, matchLength: linkPath.length }
      }
    }
  }

  return { group: bestMatch.group, link: bestMatch.link }
}

// 高亮匹配的链接
function highlightMatchingLink(sidebar: Element, currentPath: string) {
  // 先移除所有自定义高亮
  sidebar.querySelectorAll('.VPSidebarItem a.custom-active').forEach(el => {
    el.classList.remove('custom-active')
  })

  const { link } = findBestMatchingLink(sidebar, currentPath)

  // 如果找到匹配的链接且它没有 is-active 类，添加自定义高亮
  if (link && !link.classList.contains('is-active')) {
    link.classList.add('custom-active')
  }
}

// 收起非当前页面所在的分组
function collapseOtherGroups() {
  const sidebar = document.querySelector('.VPSidebar')
  if (!sidebar) return

  const currentPath = route.path
  const { group: matchingGroup } = findBestMatchingLink(sidebar, currentPath)

  // 高亮匹配的链接
  highlightMatchingLink(sidebar, currentPath)

  // 找到所有可折叠的分组
  const allGroups = sidebar.querySelectorAll('.VPSidebarItem.level-0.collapsible')

  allGroups.forEach((group) => {
    const isCollapsed = group.classList.contains('collapsed')
    const titleEl = group.querySelector(':scope > .item > .indicator, :scope > .item > .caret')

    // 判断当前分组是否应该展开
    const shouldExpand = group === matchingGroup

    if (shouldExpand && isCollapsed && titleEl) {
      // 当前页面所在的组需要展开，点击展开
      (titleEl as HTMLElement).click()
    } else if (!shouldExpand && !isCollapsed && titleEl) {
      // 其他组需要收起，点击收起
      (titleEl as HTMLElement).click()
    }
  })
}

onMounted(() => {
  // 从 localStorage 恢复状态
  const savedCollapsed = localStorage.getItem('vp-sidebar-collapsed')

  if (savedCollapsed === 'true') {
    sidebarCollapsed.value = true
  }

  applyStyles()

  // 页面加载后收起其他分组
  setTimeout(collapseOtherGroups, 200)
})

// 监听路由变化
watch(() => route.path, () => {
  // 增加延迟，确保 VitePress 已更新 DOM
  setTimeout(collapseOtherGroups, 300)
})

watch(sidebarCollapsed, () => {
  applyStyles()
  localStorage.setItem('vp-sidebar-collapsed', String(sidebarCollapsed.value))
})

function applyStyles() {
  if (sidebarCollapsed.value) {
    document.body.classList.add('sidebar-collapsed')
  } else {
    document.body.classList.remove('sidebar-collapsed')
  }
}

function toggleSidebar() {
  sidebarCollapsed.value = !sidebarCollapsed.value
}
</script>

<template>
  <Layout>
    <template #doc-before>
      <button class="collapse-btn" @click="toggleSidebar" :title="sidebarCollapsed ? '展开' : '收起'">
        <span class="collapse-icon" :class="{ collapsed: sidebarCollapsed }">‹</span>
      </button>
    </template>
  </Layout>
</template>

<style>
/* 折叠按钮 */
.collapse-btn {
  position: fixed;
  left: 8px;
  top: 72px;
  z-index: 100;
  width: 24px;
  height: 24px;
  border: 1px solid var(--vp-c-divider);
  border-radius: 50%;
  background: var(--vp-c-bg);
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  color: var(--vp-c-text-2);
  transition: all 0.2s;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.collapse-btn:hover {
  border-color: var(--vp-c-brand-1);
  color: var(--vp-c-brand-1);
}

.collapse-icon {
  transition: transform 0.2s;
}

.collapse-icon.collapsed {
  transform: rotate(180deg);
}

/* 侧边栏折叠时的样式 */
@media (min-width: 960px) {
  /* 增加侧边栏宽度以显示完整命名空间 */
  :root {
    --vp-sidebar-width: 640px;
  }

  .VPSidebar {
    width: var(--vp-sidebar-width) !important;
    max-width: var(--vp-sidebar-width) !important;
  }

  .VPContent.has-sidebar {
    padding-left: var(--vp-sidebar-width) !important;
  }

  .sidebar-collapsed .VPSidebar {
    transform: translateX(-100%);
  }

  .sidebar-collapsed .VPContent.has-sidebar {
    padding-left: 32px !important;
  }

  /* 折叠时按钮在左边 */
  .sidebar-collapsed .collapse-btn {
    left: 8px;
  }

  /* 未折叠时，按钮贴在侧边栏右边缘 */
  body:not(.sidebar-collapsed) .collapse-btn {
    left: calc(var(--vp-sidebar-width) - 12px);
  }
}

/* 移动端隐藏自定义控件 */
@media (max-width: 959px) {
  .collapse-btn {
    display: none;
  }
}

/* 侧边栏文本不截断 */
.VPSidebar .VPSidebarItem .text {
  white-space: nowrap;
  overflow: visible;
  text-overflow: clip;
}

/* 右侧页面导航文本不截断 */
.VPDocAsideOutline .outline-link {
  white-space: nowrap;
  overflow: visible;
  text-overflow: clip;
}

/* 修复侧边栏阴影覆盖到顶部导航栏的问题 */
.VPSidebar {
  z-index: 1;
  top: var(--vp-nav-height) !important;
  height: calc(100vh - var(--vp-nav-height)) !important;
}

/* 隐藏侧边栏的所有渐变阴影 */
.VPSidebar::before,
.VPSidebar::after,
.VPSidebarNav::before,
.VPSidebarNav::after,
.VPSidebar .nav::before,
.VPSidebar .nav::after,
.VPSidebar .group::before,
.VPSidebar .group::after {
  display: none !important;
  opacity: 0 !important;
  visibility: hidden !important;
}

/* 隐藏左侧可能的背景渐变 */
.VPLocalNav::before,
.VPLocalNav::after {
  display: none !important;
}

/* 确保顶部导航栏在最上层 */
.VPNav {
  z-index: 10;
}

/* 隐藏左侧导航栏区域的分隔线 */
.VPNavBar .divider-line {
  display: none !important;
}

/* 让内容区域的分隔线延长到整行 */
.VPNavBar .content {
  position: relative;
}

.VPNavBar .content::after {
  content: '';
  position: absolute;
  left: -100vw;
  right: 0;
  bottom: 0;
  height: 1px;
  background: var(--vp-c-divider);
}

/* 移除侧边栏区域的背景渐变效果 */
.VPSidebar,
.VPSidebar * {
  background-image: none !important;
}

/* 自定义高亮样式（用于自动生成的 API 文档页面） */
.VPSidebar .VPSidebarItem a.custom-active {
  color: var(--vp-c-brand-1) !important;
  font-weight: 500;
}

.VPSidebar .VPSidebarItem a.custom-active .text {
  color: var(--vp-c-brand-1) !important;
}
</style>
