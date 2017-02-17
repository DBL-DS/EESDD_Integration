using EESDD.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    public class Recorder
    {
        enum AreaState
        {
            Outer,
            In,
            Inner,
            Out
        }

        private Scene scene;
        private Mode mode;
        private Exp exp;
        private List<AreaExp> areas;

        private Svframe currentFrame;
        private BrakeAct currentBrake;
        private Reaction currentReaction;

        private AreaState areaState;
        private int currentArea;
        private bool START;

        private float _ZERO = 0.000001f;
        private float _MINSTARTINTERVAL = 1;

        public void Start(Scene scene, Mode mode)
        {
            this.scene = scene;
            this.mode = mode;

            Init();
        }

        private void Init()
        {
            exp = new Exp(scene.Name, mode.Name);
            exp.Tic();

            areas = new List<AreaExp>();
            foreach (var item in scene.AreaTitle)
            {
                AreaExp area = new AreaExp();
                area.AreaTitle = item;
                areas.Add(area);
            }

            currentFrame = new Svframe(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
            currentBrake = null;
            currentReaction = null;

            areaState = AreaState.Outer;
            currentArea = 0;

            START = false;
        }

        public Svframe CurrentFrame
        {
            get { return currentFrame; }
        }

        private AreaExp TotalArea
        {
            get { return areas[0]; }
        }

        private AreaExp CurrentArea
        {
            get
            {
                if (currentArea > areas.Count - 1)
                    areas.Add(new AreaExp());
                return areas[currentArea];
            }
        }

        public float BrakeDistance
        {
            get
            {
                if (currentBrake == null)
                    return 0;

                return currentBrake.BrakeDistanceNow(currentFrame);
            }
        }

        public float ReactionTime
        {
            get
            {
                if (currentReaction == null)
                    return 0;

                return currentReaction.ReactionTimeNow(currentFrame);
            }
        }

        public void Record(Svframe frame)
        {
            if (DetectStart(frame))
            {
                RecordArea(frame);
                RecordBrake(frame);
                RecordReaction(frame);

                currentFrame = frame;
            }
        }

        private bool DetectStart(Svframe frame)
        {
            if (START)
                return true;
            else if (frame.Time < _MINSTARTINTERVAL)
                START = true;

            return START;
        }

        private void ChangeState(Svframe frame)
        {
            if (frame.Area > _ZERO && currentFrame.Area <= _ZERO)
            {
                areaState = AreaState.In;
                ++currentArea;
            }
            else if (frame.Area <= _ZERO && currentFrame.Area > _ZERO)
                areaState = AreaState.Out;
            else if (frame.Area <= _ZERO)
                areaState = AreaState.Outer;
            else
                areaState = AreaState.Inner;
        }

        private void RecordArea(Svframe frame)
        {
            TotalArea.AddFrame(frame);

            ChangeState(frame);
            if (areaState == AreaState.In || areaState == AreaState.Inner)
                CurrentArea.AddFrame(frame);
        }

        private void RecordBrake(Svframe frame)
        {
            if (frame.Braking >= _ZERO && CurrentFrame.Braking < _ZERO)
            {
                currentBrake = new BrakeAct();
                currentBrake.StartAct(frame);
            }

            if (frame.Braking < _ZERO && CurrentFrame.Braking >= _ZERO)
            {
                currentBrake.EndAct(currentFrame);

                TotalArea.AddBrakeAct(currentBrake);
                if (areaState == AreaState.Inner)
                    CurrentArea.AddBrakeAct(currentBrake);
            }
        }

        private void RecordReaction(Svframe frame)
        {
            if (frame.Reacting >= _ZERO && CurrentFrame.Reacting < _ZERO)
            {
                currentReaction = new Reaction();
                currentReaction.StartAct(frame);
            }

            if (frame.Reacting < _ZERO && CurrentFrame.Reacting >= _ZERO)
            {
                currentReaction.EndAct(currentFrame);

                TotalArea.AddReaction(currentReaction);
                if (areaState == AreaState.Inner)
                    CurrentArea.AddReaction(currentReaction);
            }
        }

        public Exp Stop()
        {
            exp.Toc();
            exp.Areas = areas;

            return exp;
        }
    }
}
