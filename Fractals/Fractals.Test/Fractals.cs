using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFrame.Base.Contracts.Math;
using AFrame.Base.Math.Graphing;
using AFrame.Base.Math;
using AFrame.Base.Math.Models;
using AFrame.Base.Contracts.Abstractions;
using AFrame.Base;
using AFrame.Base.Contracts.Math.Graphing;
using AFrame.Base.Contracts.Math.Forms;
using Fractals;
using Fractals.Contracts;

using Xunit;


namespace Fractals.Test
{
    public class Fractals
    {

        #region Tests

        [Fact]
        public void QuadricKochIslandTest()
        {
            var init = new SquareInitiator();
            var gen = new QuadricKochIslandGenerator();

            this.TestCore(init, gen);
        }

        private void TestCore(IInitiator initiator, IGenerator generator)
        {
            var evolver = new SimpleEvolver(initiator, generator);
            var scaler = new SimpleScaler();
            var grapher = new Grapher();

            while(true)
                evolver.Step();
        }

        #endregion Tests


        #region Forms

        public class SimpleSquare : IDescribableGraph<SimpleSquare.ISimpleSquareDescription>
        {
            //similar to inits params more or less,
            public ISimpleSquareDescription Description => throw new NotImplementedException();

            public IGraph Graph => _graph;

            public interface ISimpleSquareDescription : IGraphDescription { }

            private static readonly IPoint2D[] _points = new Point2D[]
            {
                    new() { X = -1.0f, Y = -1.0f },
                    new() { X = 1.0f, Y = -1.0f },
                    new() { X = 1.0f, Y = 1.0f },
                    new() { X = -1.0f, Y = 1.0f }
            };

            private static readonly IEdge2D[] _edges = new Edge2D[]
            {
                    new Edge2D() { Item1 = _points[0], Item2 = _points[1] },
                    new Edge2D() { Item1 = _points[1], Item2 = _points[2] },
                    new Edge2D() { Item1 = _points[2], Item2 = _points[3] },
                    new Edge2D() { Item1 = _points[3], Item2 = _points[0] }
            };

            private static readonly SetGrapht2D _graph = new()
            {
                Vertices = new HashSet<IPoint2D>(_points),
                Edges = new HashSet<IEdge<IPoint2D>>(_edges.Cast<IEdge<IPoint2D>>())
            };
        }


        //need better abstractions for "forms", idk what correct nomen even is -ag

        public class QuadricKochIslandGeneratorFormGraph : QuadricKochIslandGeneratorFormGraph.IQuadricKochIslandGeneratorFormGraph
        {
            public IGraph Graph => throw new NotImplementedException();

            public interface IQuadricKochIslandGeneratorFormGraph : IFormGraph { }
        }

        public class SimpleSquareKoch : IDescribableGraph<SimpleSquareKoch.ISimpleSquareKochDescription>
        {
            //should have all params, need similar for gen... -ag
            public ISimpleSquareKochDescription Description => throw new NotImplementedException();

            public IGraph Graph => _graph;


            private static readonly IPoint2D[] _points = new Point2D[]
            {
                    new() { X = -1.0f, Y = -1.0f },
                    new() { X = 1.0f, Y = -1.0f },
                    new() { X = 1.0f, Y = 1.0f },
                    new() { X = -1.0f, Y = 1.0f }
            };

            private static readonly IEdge2D[] _edges = new Edge2D[]
            {
                    new Edge2D() { Item1 = _points[0], Item2 = _points[1] },
                    new Edge2D() { Item1 = _points[1], Item2 = _points[2] },
                    new Edge2D() { Item1 = _points[2], Item2 = _points[3] },
                    new Edge2D() { Item1 = _points[3], Item2 = _points[0] }
            };

            private static readonly SetGrapht2D _graph = new()
            {
                Vertices = new HashSet<IPoint2D>(_points),
                Edges = new HashSet<IEdge<IPoint2D>>(_edges.Cast<IEdge<IPoint2D>>())
            };

            public interface ISimpleSquareKochDescription : IGraphDescription { }
        }

        #endregion Forms



        #region Generators 
        public class QuadricKochIslandGenerator : QuadricKochIslandGenerator.IQuadricKochIslandGenerator
        {
            public IGeneratorDescription Description => throw new NotImplementedException();


            //private readonly IGraph graph = (IGraph)new ();
            public IGraph Graph => this.graph;


            public interface IQuadricKochIslandGenerator : IGenerator { }

        }
        #endregion Generators

        #region Initiators 


        public class SquareInitiator : SquareInitiator.ISquareInitiator
        {
            public IInitiatorDescription Description => (IInitiatorDescription)((IDescribableGraph<SimpleSquare.ISimpleSquareDescription>)this.graph).Description;

            private readonly IGraph graph = (IGraph)new SimpleSquare();
            public IGraph Graph => this.graph;

            public interface ISquareInitiator : IInitiator { }

        }


        #endregion Initiators

        #region Evolvers



        //simplest case is to have instances self-evolve, but outside evolvers usually can be useful 
        //broader fw default will be self-evolve prob, but for testing cases now ... 
        //also prob use defn-activation pattern

        public class SimpleEvolver
        {
            private readonly IInitiator initiator;
            private readonly IGenerator generator;

            public SimpleEvolver(IInitiator inititator, IGenerator generator)
            {
                this.initiator = initiator;
                this.generator = generator;
            }

            public void Step()
            {

            }


        }




        #endregion Evolvers 

        #region Scalers

        public class SimpleScaler
        {

        }

        #endregion Scalers

        #region Factories
        #endregion Factories

        #region Graphicalization

        public class Grapher
        {
            //wn 
        }

        #endregion Graphicalization
    }
}
