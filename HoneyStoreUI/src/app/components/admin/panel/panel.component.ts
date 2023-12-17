import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';

@Component({
  selector: 'app-panel',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    MatTabsModule,
    RouterModule],
  templateUrl: './panel.component.html',
  styleUrl: './panel.component.css'
})
export class PanelComponent {
  navLinks: any[] = [
    {
      label: 'Продукція',
      path: './products',
      index: 0
    },
    {
      label: 'Замовлення',
      path: './orders',
      index: 1
    },
    {
      label: 'Користувачі',
      path: './users',
      index: 2
    }
  ];
  activeLinkIndex: number = 0;

  constructor(private router: Router) {
    this.router.events.subscribe(() => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.path === '.' + this.router.url));
    });
  }
}
