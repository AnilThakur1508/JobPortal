import { Component } from '@angular/core';
import { CVUploaderComponent } from "../../shared/cv-uploader/cv-uploader.component";
import { WorkComponent } from "../../shared/work/work.component";
import { TestimonialComponent } from "../../shared/testimonial/testimonial.component";
import { SupportComponent } from "../../shared/support/support.component";
import { BlogComponent } from "../../shared/blog/blog.component";

@Component({
  selector: 'app-home',
  imports: [CVUploaderComponent, WorkComponent, TestimonialComponent, SupportComponent, BlogComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
