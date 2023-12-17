import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Comment } from '../../../models'
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService, CommentService } from '../../../services';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { EditorModule } from '@tinymce/tinymce-angular';

@Component({
  selector: 'app-comment-dialog',
  standalone: true,
  imports: [CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    EditorModule,
    MatButtonModule],
  templateUrl: './comment-dialog.component.html',
  styleUrl: './comment-dialog.component.css'
})
export class CommentDialogComponent {
  @Input() productId: number = 0;
  @Output() commentAdded = new EventEmitter();
  comment: Comment = new Comment();
  commentForm: FormGroup = this.createFormGroup(this.comment);
  rating: number = 0;


  constructor(private authSvc: AuthenticationService, private commentSvc: CommentService, private formBuilder: FormBuilder) {
  }

  onRate(rate: number) {
    this.rating = rate;
  }

  onSave() {

    if (this.commentForm.invalid) {
      return;
    }

    const commentValue = this.commentForm.value;
    this.comment = new Comment(this.authSvc.currentUserValue?.id, this.productId, commentValue.content, this.rating)

    this.commentSvc.post(this.comment).subscribe(() => {
        this.commentAdded.emit();
      }
    );

    this.comment = new Comment();
    this.commentForm = this.createFormGroup(this.comment)
  }

  createFormGroup(comment: Comment): FormGroup {
    return this.formBuilder.group({
      content: [comment.content, [Validators.required]]
    })
  }
}
