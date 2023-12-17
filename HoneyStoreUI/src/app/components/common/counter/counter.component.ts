import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-counter',
  standalone: true,
  imports: [CommonModule,
  FormsModule,
  ReactiveFormsModule,
  MatCardModule,
  MatButtonModule,
  MatIconModule,
  MatInputModule,
  MatFormFieldModule],
  templateUrl: './counter.component.html',
  styleUrl: './counter.component.css'
})
export class CounterComponent {
  @Input() minValue: number = 1;
  @Input() maxValue: number = 100;
  @Input() currentValue: number = 1;
  @Output() counterValue = new EventEmitter<number>();
  counterControl: FormControl = new FormControl();

  constructor() {
    this.counterControl = new FormControl(this.currentValue,
      [Validators.min(this.minValue - 1),
      Validators.max(this.maxValue + 1)])
  }

  increase() {
    if (this.currentValue >= this.maxValue)
      return;
    this.counterValue.emit(++this.currentValue);
  }

  decrease() {
    if (this.currentValue <= this.minValue)
      return;
    this.counterValue.emit(--this.currentValue)
  }
}
