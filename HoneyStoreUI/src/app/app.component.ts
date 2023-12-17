import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainContainerComponent, PanelComponent, ProductContainerComponent, AdminUserListComponent, AboutUsComponent, OrderComponent, PrivacyPolicyComponent, AdminOrderListComponent, OrderListComponent } from './components';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, 
    MainContainerComponent, 
    ProductContainerComponent,
    PanelComponent,
    AboutUsComponent,
    PrivacyPolicyComponent,
    OrderComponent,
    AdminOrderListComponent,
    OrderListComponent,
    AdminUserListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'HoneyStoreUI';
}
