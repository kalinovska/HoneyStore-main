import { Routes, mapToCanActivate } from '@angular/router';
import { AboutUsComponent, AdminOrderListComponent, AdminProductListComponent, AdminUserListComponent, LoginComponent, OrderComponent, OrderListComponent, PanelComponent, PrivacyPolicyComponent, ProductContainerComponent, ProductDetailComponent, RegisterComponent } from './components';

export const routes: Routes = [
    {
        path: '',
        component: ProductContainerComponent,
        // canActivate: [AuthGuard]
        
      },
    {
        path: 'products',
        component: ProductContainerComponent
    },
    {
        path: 'detail/:id',
        component: ProductDetailComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
        //canActivate: mapToCanActivate([AuthGuard])
    },
    {
      path: 'order',
      component: OrderComponent
    },
    {
      path: 'orders',
      component: OrderListComponent
    },
    {
      path: 'about-us',
      component: AboutUsComponent
    },
    {
      path: 'privacy-policy',
      component: PrivacyPolicyComponent
    },
    {
        path: 'panel',
        component: PanelComponent,
        children: [
           {
             path: 'products',
             component: AdminProductListComponent
           },
           {
            path: 'orders',
            component: AdminOrderListComponent
           },
           {
            path: 'users',
            component: AdminUserListComponent
           }    
        ]
      },
  // otherwise redirect to home
   { path: '**', redirectTo: '/products', pathMatch: 'full' }
];
