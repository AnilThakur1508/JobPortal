import { Component } from '@angular/core';
import { SupportComponent } from "../../shared/support/support.component";
import { WorkComponent } from "../../shared/work/work.component";
import { TestimonialComponent } from "../../shared/testimonial/testimonial.component";
import { CVUploaderComponent } from "../../shared/cv-uploader/cv-uploader.component";
import { BlogComponent } from "../../shared/blog/blog.component";

@Component({
  selector: 'app-about',
  imports: [SupportComponent, WorkComponent, TestimonialComponent, CVUploaderComponent, BlogComponent],
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})
export class AboutComponent {

}
