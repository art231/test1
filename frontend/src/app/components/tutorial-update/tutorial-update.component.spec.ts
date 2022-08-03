import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TutorialUpdateComponent } from './tutorial-update.component';

describe('TutorialDetailsComponent', () => {
  let component: TutorialUpdateComponent;
  let fixture: ComponentFixture<TutorialUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TutorialUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TutorialUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
