import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CVUploaderComponent } from './cv-uploader.component';

describe('CVUploaderComponent', () => {
  let component: CVUploaderComponent;
  let fixture: ComponentFixture<CVUploaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CVUploaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CVUploaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
