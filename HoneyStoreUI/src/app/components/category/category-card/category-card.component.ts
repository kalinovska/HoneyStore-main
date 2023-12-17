import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { Category } from '../../../models';
import { CategoryService } from '../../../services';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [CommonModule,
  MatCardModule,
  MatButtonModule,
  MatDividerModule,
  MatListModule],
  templateUrl: './category-card.component.html',
  styleUrl: './category-card.component.css'
})
export class CategoryCardComponent {
  @Output() selectedCategory = new EventEmitter<number>();
  categories: Category[] = [];
  
  constructor(private categorySvc: CategoryService) {
    this.categorySvc.getAll().subscribe(data => {
      this.categories = data;
    })
  }

  emitCategory(categoryId: number) {
    this.selectedCategory.emit(categoryId);
  }
}
