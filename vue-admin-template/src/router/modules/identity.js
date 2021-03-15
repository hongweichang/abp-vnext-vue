/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const identityRouter = {
  path: '/identity',
  component: Layout,
  redirect: 'noRedirect',
  alwaysShow: true,
  name: 'Identity',
  meta: {
    title: '身份认证管理',
    icon: 'user',
    policy: ''
  },
  children: [
    {
      path: 'roles',
      component: () => import('@/views/identity/roles'),
      name: 'Roles',
      meta: { title: '角色', policy: 'AbpIdentity.Roles' }
    },
    {
      path: 'users',
      component: () => import('@/views/identity/users'),
      name: 'Users',
      meta: { title: '用户', policy: 'AbpIdentity.Users' }
    }
  ]
}
export default identityRouter
